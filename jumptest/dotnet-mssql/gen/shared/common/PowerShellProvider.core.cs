using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace jumptest
{
    /// <summary>
    /// PowerShell script provider that executes PowerShell scripts via system call
    /// </summary>
    public class PowerShellProvider : IScriptProvider
    {
        /// <summary>
        /// Compiles and executes PowerShell script source code with the provided context
        /// </summary>
        /// <param name="sourceCode">The PowerShell script source code to execute</param>
        /// <param name="context">The script execution context</param>
        public void CompileAndExecute(string sourceCode, ScriptContext context)
        {
            if (string.IsNullOrEmpty(sourceCode))
            {
                throw new ArgumentException("PowerShell script source code cannot be null or empty.", nameof(sourceCode));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string tempScriptFile = null;
            string tempContextFile = null;

            try
            {
                // Clear any previous exception
                context.ScriptException = null;

                // Create temporary script file
                tempScriptFile = Path.GetTempFileName();
                tempScriptFile = Path.ChangeExtension(tempScriptFile, ".ps1");
                
                // Create temporary context file (JSON) to pass ScriptContext to PowerShell
                tempContextFile = Path.GetTempFileName();
                tempContextFile = Path.ChangeExtension(tempContextFile, ".json");
                
                // Serialize ScriptContext to JSON
                var contextData = new Dictionary<string, object>
                {
                    { "ScriptResult", context.ScriptResult },
                    { "retCode", context.retCode },
                    { "Args", context.Args ?? new Dictionary<string, object>() }
                };
                
                // Add Transaction as a dictionary if it exists
                if (context.Transaction != null)
                {
                    var transactionDict = new Dictionary<string, object>();
                    foreach (var kvp in context.Transaction)
                    {
                        transactionDict[kvp.Key] = kvp.Value;
                    }
                    contextData["Transaction"] = transactionDict;
                }
                
                var contextJson = JsonSerializer.Serialize(contextData);
                // Use UTF-8 without BOM to avoid PowerShell JSON parsing issues
                File.WriteAllText(tempContextFile, contextJson, new UTF8Encoding(false));

                // Create PowerShell script that loads context and executes user script
                var contextFileEscaped = tempContextFile.Replace("'", "''").Replace("\\", "\\\\");
                var powershellScript = "# Load ScriptContext from JSON file\n" +
                    "$contextFile = '" + contextFileEscaped + "'\n" +
                    "try {\n" +
                    "    $contextJson = Get-Content -Path $contextFile -Raw -Encoding UTF8\n" +
                    "    $contextData = $contextJson | ConvertFrom-Json\n" +
                    "    # Make context available as global variable\n" +
                    "    $global:ScriptContext = $contextData\n" +
                    "    $global:Context = $contextData\n" +
                    "} catch {\n" +
                    "    Write-Error \"Error loading context: $_\"\n" +
                    "    exit 1\n" +
                    "}\n" +
                    "\n" +
                    "# User's script code\n" +
                    sourceCode;

                // Write the complete PowerShell script to the temp file
                // Use UTF-8 without BOM for consistency
                File.WriteAllText(tempScriptFile, powershellScript, new UTF8Encoding(false));

                // Execute PowerShell script
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{tempScriptFile}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                string stdout = string.Empty;
                string stderr = string.Empty;
                int exitCode = 0;
                Exception processException = null;

                using (var process = new Process())
                {
                    process.StartInfo = processStartInfo;
                    
                    // Capture output
                    var outputBuilder = new StringBuilder();
                    var errorBuilder = new StringBuilder();
                    
                    // Hook stdout with delegate to write to Console
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Console.WriteLine(e.Data);
                            outputBuilder.AppendLine(e.Data);
                        }
                    };
                    
                    // Hook stderr with delegate to write to Console
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Console.Error.WriteLine(e.Data);
                            errorBuilder.AppendLine(e.Data);
                        }
                    };

                    try
                    {
                        process.Start();
                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();
                        process.WaitForExit();
                        exitCode = process.ExitCode;
                    }
                    catch (Exception ex)
                    {
                        processException = ex;
                    }

                    stdout = outputBuilder.ToString();
                    stderr = errorBuilder.ToString();
                }

                // Check for process start errors
                if (processException != null)
                {
                    throw new Exception($"Failed to start PowerShell. Make sure PowerShell is installed and in PATH. Error: {processException.Message}", processException);
                }

                // Check for script execution errors
                if (exitCode != 0 || !string.IsNullOrEmpty(stderr))
                {
                    var errorMessage = $"PowerShell script exited with code {exitCode}";
                    if (!string.IsNullOrEmpty(stderr))
                    {
                        errorMessage += $"\nError output:\n{stderr}";
                    }
                    throw new Exception(errorMessage);
                }

                // Store output in context
                if (!string.IsNullOrEmpty(stdout))
                {
                    context.ScriptResult = stdout;
                }
            }
            catch (Exception ex)
            {
                // Capture the exception in the context
                context.ScriptException = ex;
                Logger.Error($"Exception thrown in PowerShell script: {ex.Message}", ex);
                throw;
            }
            finally
            {
                // Clean up temporary files
                try
                {
                    if (tempScriptFile != null && File.Exists(tempScriptFile))
                    {
                        File.Delete(tempScriptFile);
                    }
                }
                catch { }

                try
                {
                    if (tempContextFile != null && File.Exists(tempContextFile))
                    {
                        File.Delete(tempContextFile);
                    }
                }
                catch { }
            }
        }
    }
}

