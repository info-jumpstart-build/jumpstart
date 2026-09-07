namespace jumptest
{
    /// <summary>
    /// Interface that compiled scripts must implement
    /// </summary>
    public interface IScript
    {
        /// <summary>
        /// Executes the script with the provided context
        /// </summary>
        /// <param name="context">The script execution context</param>
        void Execute(ScriptContext context);
    }
}
