using System;

namespace jumptest
{
    /// <summary>
    /// Factory for creating script providers based on script type
    /// </summary>
    public static class ScriptProviderFactory
    {
        /// <summary>
        /// Creates the appropriate script provider based on script_type_id
        /// </summary>
        /// <param name="scriptTypeId">The script type ID (1 = CSharp, 2 = PowerShell)</param>
        /// <returns>An instance of IScriptProvider</returns>
        /// <exception cref="NotSupportedException">Thrown when scriptTypeId is not supported</exception>
        public static IScriptProvider CreateProvider(long scriptTypeId)
        {
            switch (scriptTypeId)
            {
                case 1: // CSharp
                    return new CSharpCompiler();
                
                case 2: // PowerShell
                    return new PowerShellProvider();
                
                case 3: // Python
                    return new PythonProvider();
                
                default:
                    throw new NotSupportedException($"Script type ID {scriptTypeId} is not supported. Supported types: 1 (CSharp), 2 (PowerShell), 3 (Python)");
            }
        }
    }
}

