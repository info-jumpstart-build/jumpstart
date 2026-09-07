using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace jumptest
{
   public class ScriptTest : Script
   {
        public ScriptTest(string name, string source, long scriptTypeId = 1) : base(new Script())
        {
            this.name = name;
            this.source = source;
            this.script_type_id = scriptTypeId;
        }
   }

    public class TestModel
    {
        public string msg = string.Empty;
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Testing Script Providers via ScriptHost ===\n");
            
            var scriptHost = new ScriptHost();
            var context = new ScriptContext();
            context.Initialize();
            
            Console.WriteLine("--- C# Script Tests ---");
            TestCSharpScripts(scriptHost, context);
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Console.WriteLine("\n--- PowerShell Script Tests ---");
                TestPowerShellScripts(scriptHost, context);
            }
            else
            {
                Console.WriteLine("\n--- PowerShell Script Tests ---");
                Console.WriteLine("  ⚠ Skipped: PowerShell tests only run on Windows\n");
            }
            
            Console.WriteLine("\n--- Python Script Tests ---");
            TestPythonScripts(scriptHost, context);
            
            Console.WriteLine("\n=== All Tests Complete ===");
        }
        
        static void TestCSharpScripts(ScriptHost scriptHost, ScriptContext context)
        {
            Example1_SimpleScript(scriptHost, context);
            Example2_LoopScript(scriptHost, context);
            Example3_AdvancedScript(scriptHost, context);
            Example4_ExceptionScript(scriptHost, context);
            Example5_GenericCollection(scriptHost, context);
        }
        
        static void TestPowerShellScripts(ScriptHost scriptHost, ScriptContext context)
        {
            PowerShellExample1_SimpleScript(scriptHost, context);
            PowerShellExample2_LoopScript(scriptHost, context);
            PowerShellExample3_ContextAccess(scriptHost, context);
            PowerShellExample4_ExceptionScript(scriptHost, context);
        }
        
        static void TestPythonScripts(ScriptHost scriptHost, ScriptContext context)
        {
            PythonExample1_SimpleScript(scriptHost, context);
            PythonExample2_LoopScript(scriptHost, context);
            PythonExample3_ContextAccess(scriptHost, context);
            PythonExample4_ExceptionScript(scriptHost, context);
        }

        /// <summary>
        /// Example 1: Simple script execution with complete class definition
        /// </summary>
        static void Example1_SimpleScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 1: Simple C# Script");
            var example1 = @"
using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class SimpleScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine(""Hello, World from compiled C# script!"");
        }
    }
}";
            var script = new ScriptTest("SimpleScript", example1, 1); // script_type_id = 1 (CSharp)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ C# script executed successfully\n");
        }

        /// <summary>
        /// Example 2: Script with loop
        /// </summary>
        static void Example2_LoopScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 2: C# Script with Loop");
            var scriptSource = @"
using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class MyScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine(""Executing C# MyScript"");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($""  Iteration {i}"");
            }
        }
    }
}";
            var script = new ScriptTest("LoopScript", scriptSource, 1); // script_type_id = 1 (CSharp)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ C# loop script executed successfully\n");
        }

        /// <summary>
        /// Example 3: Script that uses external types with complete class definition
        /// </summary>
        static void Example3_AdvancedScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 3: C# Script with External Types");
            var advancedScript = @"
using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class AdvancedScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            var model = new jumptest.TestModel();
            model.msg = ""Hello from C# script!"";
            Console.WriteLine(model.msg);
        }
    }
}";
            var script = new ScriptTest("AdvancedScript", advancedScript, 1); // script_type_id = 1 (CSharp)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ C# advanced script executed successfully\n");
        }

        /// <summary>
        /// Example 4: Script that throws an exception
        /// </summary>
        static void Example4_ExceptionScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 4: C# Script Exception Handling");
            var exceptionScript = @"
using System;
using System.IO;
using jumptest;

namespace Generated
{
    public class ExceptionScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine(""About to throw an exception..."");
            throw new Exception(""This is a test exception from a C# script!"");
        }
    }
}";
            try
            {
                var script = new ScriptTest("ExceptionScript", exceptionScript, 1); // script_type_id = 1 (CSharp)
                scriptHost.InvokeSync(context, script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✓ Caught exception: {ex.Message}");
                Console.WriteLine($"  Exception type: {ex.GetType().FullName}\n");
            }
        }

        /// <summary>
        /// Example 5: Script that uses generic collections - demonstrates automatic reference resolution
        /// </summary>
        static void Example5_GenericCollection(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 5: C# Script with Generic Collections");
            var collectionScript = @"
using System;
using System.Collections.Generic;
using jumptest;

namespace Generated
{
    public class GenericCollectionScript : IScript
    {
        public void Execute(ScriptContext context)
        {
            Console.WriteLine(""Creating a List<string> and adding elements..."");
            
            var items = new List<string>();
            items.Add(""First item"");
            items.Add(""Second item"");
            items.Add(""Third item"");
            items.Add(""Fourth item"");
            
            Console.WriteLine($""Added {items.Count} items to the list"");
            Console.WriteLine(""Iterating through the collection:"");
            
            foreach (var item in items)
            {
                Console.WriteLine($""  - {item}"");
            }
            
            Console.WriteLine(""Collection iteration complete!"");
        }
    }
}";
            var script = new ScriptTest("GenericCollectionScript", collectionScript, 1); // script_type_id = 1 (CSharp)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ C# collection script executed successfully\n");
        }
        
        // PowerShell Script Tests
        
        /// <summary>
        /// PowerShell Example 1: Simple script
        /// </summary>
        static void PowerShellExample1_SimpleScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 1: Simple PowerShell Script");
            var scriptSource = "Write-Host 'Hello, World from PowerShell script!'";
            var script = new ScriptTest("PowerShellSimpleScript", scriptSource, 2); // script_type_id = 2 (PowerShell)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ PowerShell script executed successfully\n");
        }
        
        /// <summary>
        /// PowerShell Example 2: Script with loop
        /// </summary>
        static void PowerShellExample2_LoopScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 2: PowerShell Script with Loop");
            var scriptSource = "Write-Host 'Executing PowerShell loop script'\n" +
                "for ($i = 0; $i -lt 3; $i++) {\n" +
                "    Write-Host \"  Iteration $i\"\n" +
                "}";
            var script = new ScriptTest("PowerShellLoopScript", scriptSource, 2); // script_type_id = 2 (PowerShell)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ PowerShell loop script executed successfully\n");
        }
        
        /// <summary>
        /// PowerShell Example 3: Script that accesses ScriptContext
        /// </summary>
        static void PowerShellExample3_ContextAccess(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 3: PowerShell Script Accessing Context");
            context.Args["testKey"] = "testValue";
            context.retCode = 0;
            var scriptSource = "Write-Host 'Accessing ScriptContext from PowerShell'\n" +
                "Write-Host \"ScriptContext.retCode: $($ScriptContext.retCode)\"\n" +
                "if ($ScriptContext.Args -and $ScriptContext.Args.testKey) {\n" +
                "    Write-Host \"ScriptContext.Args['testKey']: $($ScriptContext.Args.testKey)\"\n" +
                "}";
            var script = new ScriptTest("PowerShellContextScript", scriptSource, 2); // script_type_id = 2 (PowerShell)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ PowerShell context access script executed successfully\n");
        }
        
        /// <summary>
        /// PowerShell Example 4: Script that throws an exception
        /// </summary>
        static void PowerShellExample4_ExceptionScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 4: PowerShell Script Exception Handling");
            var scriptSource = "Write-Host 'About to throw an exception...'\n" +
                "throw 'This is a test exception from a PowerShell script!'";
            try
            {
                var script = new ScriptTest("PowerShellExceptionScript", scriptSource, 2); // script_type_id = 2 (PowerShell)
                scriptHost.InvokeSync(context, script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✓ Caught exception: {ex.Message}");
                Console.WriteLine($"  Exception type: {ex.GetType().FullName}\n");
            }
        }
        
        // Python Script Tests
        
        /// <summary>
        /// Python Example 1: Simple script
        /// </summary>
        static void PythonExample1_SimpleScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 1: Simple Python Script");
            var scriptSource = "print('Hello, World from Python script!')";
            var script = new ScriptTest("PythonSimpleScript", scriptSource, 3); // script_type_id = 3 (Python)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ Python script executed successfully\n");
        }
        
        /// <summary>
        /// Python Example 2: Script with loop
        /// </summary>
        static void PythonExample2_LoopScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 2: Python Script with Loop");
            var scriptSource = "print('Executing Python loop script')\n" +
                "for i in range(3):\n" +
                "    print(f'  Iteration {i}')";
            var script = new ScriptTest("PythonLoopScript", scriptSource, 3); // script_type_id = 3 (Python)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ Python loop script executed successfully\n");
        }
        
        /// <summary>
        /// Python Example 3: Script that accesses ScriptContext
        /// </summary>
        static void PythonExample3_ContextAccess(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 3: Python Script Accessing Context");
            context.Args["testKey"] = "testValue";
            context.retCode = 0;
            var scriptSource = "print('Accessing ScriptContext from Python')\n" +
                "print(f'ScriptContext.retCode: {ScriptContext.retCode}')\n" +
                "if ScriptContext.Args and 'testKey' in ScriptContext.Args:\n" +
                "    print(f\"ScriptContext.Args['testKey']: {ScriptContext.Args['testKey']}\")";
            var script = new ScriptTest("PythonContextScript", scriptSource, 3); // script_type_id = 3 (Python)
            scriptHost.InvokeSync(context, script);
            Console.WriteLine("  ✓ Python context access script executed successfully\n");
        }
        
        /// <summary>
        /// Python Example 4: Script that throws an exception
        /// </summary>
        static void PythonExample4_ExceptionScript(ScriptHost scriptHost, ScriptContext context)
        {
            Console.WriteLine("Test 4: Python Script Exception Handling");
            var scriptSource = "print('About to throw an exception...')\n" +
                "raise Exception('This is a test exception from a Python script!')";
            try
            {
                var script = new ScriptTest("PythonExceptionScript", scriptSource, 3); // script_type_id = 3 (Python)
                scriptHost.InvokeSync(context, script);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✓ Caught exception: {ex.Message}");
                Console.WriteLine($"  Exception type: {ex.GetType().FullName}\n");
            }
        }
    }
}

