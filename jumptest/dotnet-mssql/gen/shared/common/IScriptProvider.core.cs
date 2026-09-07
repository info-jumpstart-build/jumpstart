namespace jumptest
{
    /// <summary>
    /// Interface for script providers that can compile and execute scripts
    /// </summary>
    public interface IScriptProvider
    {
        /// <summary>
        /// Compiles and executes script source code with the provided context
        /// </summary>
        /// <param name="sourceCode">The script source code to execute</param>
        /// <param name="context">The script execution context</param>
        void CompileAndExecute(string sourceCode, ScriptContext context);
    }
}

