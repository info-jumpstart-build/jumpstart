using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;

namespace jumptest
{
    /// <summary>
    /// Simple C# compiler wrapper that compiles and executes C# code implementing IScript.
    /// The script author is responsible for providing complete, compilable C# code.
    /// Assembly references are automatically resolved based on the using directives in the source code.
    /// </summary>
    public class CSharpCompiler : IScriptProvider
    {
        private readonly List<MetadataReference> _references;
        private readonly Dictionary<string, Type> _compiledTypes;

        public CSharpCompiler()
        {
            _compiledTypes = new Dictionary<string, Type>();
            _references = new List<MetadataReference>();
            AddDefaultReferences();
        }

        private void AddDefaultReferences()
        {
            // Force load Console to ensure its assembly is available
            _ = typeof(Console).Assembly;
            
            // Add core system assemblies explicitly first
            var coreTypes = new Type[]
            {
                typeof(object),                           // System.Runtime (core types)
                typeof(Console),                          // System.Console
                typeof(System.Linq.Enumerable),           // System.Linq
                //typeof(System.Collections.Generic.List<>), // System.Collections
                typeof(System.Text.StringBuilder),        // System.Text
                typeof(Uri),                              // System.Runtime.Extensions
                typeof(IScript)                           // Current assembly (for IScript)
            };

            foreach (var type in coreTypes)
            {
                try
                {
                    var assembly = type.Assembly;
                    if (assembly != null && !string.IsNullOrEmpty(assembly.Location))
                    {
                        var location = assembly.Location;
                        if (!_references.Any(r => r.Display == location))
                        {
                            _references.Add(MetadataReference.CreateFromFile(location));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or skip assemblies that can't be referenced
                    System.Diagnostics.Debug.WriteLine($"Failed to add reference for {type?.FullName}: {ex.Message}");
                }
            }

            // Get all currently loaded assemblies and add them
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .ToList();

            foreach (var assembly in loadedAssemblies)
            {
                try
                {
                    var location = assembly.Location;
                    if (!string.IsNullOrEmpty(location) && !_references.Any(r => r.Display == location))
                    {
                        _references.Add(MetadataReference.CreateFromFile(location));
                    }
                }
                catch
                {
                    // Skip assemblies that can't be referenced
                }
            }
        }

        public void AddAssemblyReference(Assembly assembly)
        {
            if (assembly != null && !string.IsNullOrEmpty(assembly.Location) && 
                !_references.Any(r => r.Display == assembly.Location))
            {
                _references.Add(MetadataReference.CreateFromFile(assembly.Location));
            }
        }

        public void AddAssemblyReference(string assemblyPath)
        {
            if (File.Exists(assemblyPath) && 
                !_references.Any(r => r.Display == assemblyPath))
            {
                _references.Add(MetadataReference.CreateFromFile(assemblyPath));
            }
        }

        /// <summary>
        /// Compiles C# code and returns an instance of IScript.
        /// The sourceCode must be complete, compilable C# code containing a class that implements IScript.
        /// </summary>
        /// <param name="sourceCode">Complete C# source code that implements IScript</param>
        /// <returns>Instance of the compiled script implementing IScript</returns>
        public IScript CompileAndCreate(string sourceCode)
        {
            try
            {
                var compiledType = Compile(sourceCode);
                return (IScript)Activator.CreateInstance(compiledType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create script instance: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Compiles C# code and executes it immediately.
        /// The sourceCode must be complete, compilable C# code containing a class that implements IScript.
        /// </summary>
        /// <param name="sourceCode">Complete C# source code that implements IScript</param>
        /// <param name="context">The script execution context</param>
        public void CompileAndExecute(string sourceCode, ScriptContext context)
        {
            try
            {
                var script = CompileAndCreate(sourceCode);
                script.Execute(context);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing script: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Extracts using directives from the syntax tree and resolves them to assemblies,
        /// then adds those assemblies as references.
        /// </summary>
        private void ResolveAndAddReferencesFromUsings(SyntaxTree syntaxTree)
        {
            var root = syntaxTree.GetRoot();
            var usingDirectives = root.DescendantNodes()
                .OfType<UsingDirectiveSyntax>()
                .Where(u => u.Alias == null) // Skip using aliases
                .Select(u => u.Name?.ToString())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();

            // Get all loaded assemblies
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .ToList();

            // Track which assemblies we've already added to avoid duplicates
            var addedAssemblies = new HashSet<string>(_references.Select(r => r.Display));

            // For each using directive, try to find assemblies that contain that namespace
            foreach (var namespaceName in usingDirectives)
            {
                // Skip standard system namespaces that are already covered by default references
                if (namespaceName.StartsWith("System.", StringComparison.Ordinal) ||
                    namespaceName == "System")
                {
                    continue; // Already handled by default references
                }

                bool foundAssembly = false;

                // Strategy 1: Check loaded assemblies for types in this namespace
                foreach (var assembly in loadedAssemblies)
                {
                    try
                    {
                        // Check if this assembly has types in the requested namespace
                        var hasNamespace = assembly.GetTypes()
                            .Any(t => t.Namespace == namespaceName && 
                                     !string.IsNullOrEmpty(t.Namespace) &&
                                     t.IsPublic);

                        if (hasNamespace)
                        {
                            var location = assembly.Location;
                            if (!string.IsNullOrEmpty(location) && !addedAssemblies.Contains(location))
                            {
                                _references.Add(MetadataReference.CreateFromFile(location));
                                addedAssemblies.Add(location);
                                foundAssembly = true;
                                break; // Found an assembly for this namespace, move to next
                            }
                        }
                    }
                    catch
                    {
                        // Skip assemblies that can't be inspected
                        continue;
                    }
                }

                // Strategy 2: Try to resolve a type from this namespace using Type.GetType
                if (!foundAssembly)
                {
                    try
                    {
                        // Try common type name patterns to find a type in this namespace
                        var typeName = $"{namespaceName}.Object, {namespaceName}";
                        var type = Type.GetType(typeName, false);
                        
                        if (type == null)
                        {
                            // Try with just the namespace as assembly name
                            typeName = $"{namespaceName}.{GetLastNamespaceSegment(namespaceName)}, {GetLastNamespaceSegment(namespaceName)}";
                            type = Type.GetType(typeName, false);
                        }

                        if (type != null && type.Assembly != null)
                        {
                            var assembly = type.Assembly;
                            var location = assembly.Location;
                            if (!string.IsNullOrEmpty(location) && !addedAssemblies.Contains(location))
                            {
                                _references.Add(MetadataReference.CreateFromFile(location));
                                addedAssemblies.Add(location);
                            }
                        }
                    }
                    catch
                    {
                        // Could not resolve type - namespace might not exist or assembly not loaded
                    }
                }
            }
        }

        /// <summary>
        /// Gets the last segment of a namespace (e.g., "System.IO" -> "IO")
        /// </summary>
        private string GetLastNamespaceSegment(string namespaceName)
        {
            if (string.IsNullOrEmpty(namespaceName))
                return namespaceName;

            var lastDot = namespaceName.LastIndexOf('.');
            return lastDot >= 0 ? namespaceName.Substring(lastDot + 1) : namespaceName;
        }

        /// <summary>
        /// Compiles C# code and returns the Type.
        /// The sourceCode must be complete, compilable C# code containing a class that implements IScript.
        /// </summary>
        /// <param name="sourceCode">Complete C# source code that implements IScript</param>
        /// <returns>Compiled Type implementing IScript</returns>
        public Type Compile(string sourceCode)
        {
            var cacheKey = GetCacheKey(sourceCode);
            
            // Check cache first
            if (_compiledTypes.TryGetValue(cacheKey, out var cachedType))
            {
                return cachedType;
            }

            // Parse the source code and extract using directives
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
            
            // Resolve and add assembly references based on using statements
            ResolveAndAddReferencesFromUsings(syntaxTree);
            
            var compilation = CSharpCompilation.Create(
                $"Script_{Guid.NewGuid():N}",
                new[] { syntaxTree },
                _references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                    .WithOptimizationLevel(OptimizationLevel.Release));

            // Emit assembly
            using var ms = new MemoryStream();
            var emitResult = compilation.Emit(ms);

            if (!emitResult.Success)
            {
                var errors = string.Join("\n", emitResult.Diagnostics
                    .Where(d => d.Severity == DiagnosticSeverity.Error)
                    .Select(d => $"{d.GetMessage()} (Line {d.Location.GetLineSpan().StartLinePosition.Line + 1})"));
                
                // Include reference information for debugging
                var referenceList = string.Join("\n", _references.Select(r => $"  - {r.Display}"));
                
                throw new InvalidOperationException(
                    $"Compilation failed:\n{errors}\n\nReferences ({_references.Count}):\n{referenceList}\n\nSource code:\n{sourceCode}");
            }

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = Assembly.Load(ms.ToArray());

            // Find the IScript implementation
            var scriptType = assembly.GetTypes()
                .FirstOrDefault(t => typeof(IScript).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            if (scriptType == null)
            {
                throw new InvalidOperationException(
                    $"Compiled code does not contain a class implementing IScript.\n\nSource code:\n{sourceCode}");
            }

            _compiledTypes[cacheKey] = scriptType;
            return scriptType;
        }

        private string GetCacheKey(string sourceCode)
        {
            // Generate a key from source code (simple hash)
            using var sha = System.Security.Cryptography.SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(sourceCode));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
