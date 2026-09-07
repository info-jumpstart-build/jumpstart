using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.Threading.Tasks;


namespace jumptest
{
    public class ScriptContext
    {
        public BaseObject? Transaction { get; set;}
        public Exception? ScriptException { get; set;} = null;
        public object? ScriptResult { get; set;} = null;
        public int? retCode { get; set;} = 0; // 0 = success, >= 1 = error

        public Dictionary<string, object> Args {get; private set;} 
       

        public ScriptContext()
        {
        }

        public ScriptContext(BaseObject transaction)
        {
            Transaction = transaction;
        }

        virtual public void Initialize()
        {
            Args = new Dictionary<string, object>();
        }
    }

	public class ScriptHost 
	{

		//private readonly RazorLightEngine razorEngine;

		public ScriptHost()
		{
            /*
			razorEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(BaseObject))
                .SetOperatingAssembly(typeof(BaseObject).Assembly)
                .UseMemoryCachingProvider()
                .Build();
                */

		}

		public async void Invoke<T>(T model, BaseObject s) where T : ScriptContext
		{
			if (s.ContainsKey("source"))
			{
				string source = s["source"].ToString();
				string name = s["name"].ToString();
                model.retCode = 0;

				try 
                {
                    await ExecuteScript<T>(model, name, source, s);
                }
                catch (Exception e) 
                {
                    // Exception is already captured in model.ScriptException by ExecuteScript
                    // Wrap it with context and re-throw for host code to handle
                    Exception ex = new Exception($"Error in script '{name}': {e.Message}", e);
                    throw ex;
                }
            }
		}

		protected async Task ExecuteScript<T>(T model, string name, string script, BaseObject scriptObject) where T : ScriptContext
        {
            if (model == null)
            {
                throw new Exception("Model cannot be null.");
            }

 			if (string.IsNullOrEmpty(name))
            {
                throw new Exception($"Script name cannot be null or empty.");
            }
            if (string.IsNullOrEmpty(script))
            {
                throw new Exception($"Script content cannot be null or empty.");
            }
            
            // Get script_type_id from script object, default to 1 (CSharp) for backward compatibility
            long scriptTypeId = 1; // Default to CSharp
            if (scriptObject.ContainsKey("script_type_id") && scriptObject["script_type_id"] != null)
            {
                try
                {
                    scriptTypeId = Convert.ToInt64(scriptObject["script_type_id"]);
                }
                catch
                {
                    // If conversion fails, use default
                    Logger.Warn($"Invalid script_type_id for script '{name}', defaulting to CSharp (1)");
                }
            }
            
            Logger.Debug($"Executing script '{name}' with type ID {scriptTypeId} >>" + script + "<<");
            
            try
            {
                // Clear any previous exception if execution succeeded
                model.ScriptException = null;
                
                // Get the appropriate provider from factory
                IScriptProvider provider = ScriptProviderFactory.CreateProvider(scriptTypeId);
                
                // Execute the script using the provider
                provider.CompileAndExecute(script, model);
                
                if (model.retCode != null && model.retCode != 0)
                {
                    throw new Exception("Script returned error code: " + model.retCode);
                }
            }
            catch (Exception ex)
            {
                // Capture the exception in the model for inspection by the host code
                model.ScriptException = ex;
                Logger.Error($"Exception thrown in script '{name}': {ex.Message}", ex);
                // Re-throw to allow the host code to handle it if needed
                throw;
            }
        }

		/// <summary>
		/// Synchronously executes a script. Use this for debugging and simple test scenarios.
		/// </summary>
		public void InvokeSync<T>(T model, BaseObject s) where T : ScriptContext
		{
			if (s.ContainsKey("source"))
			{
				string source = s["source"].ToString();
				string name = s["name"].ToString();
                model.retCode = 0;

				try 
                {
                    // Execute synchronously by blocking on the async task
                    ExecuteScript<T>(model, name, source, s).GetAwaiter().GetResult();
                }
                catch (Exception e) 
                {
                    // Exception is already captured in model.ScriptException by ExecuteScript
                    // Wrap it with context and re-throw for host code to handle
                    Exception ex = new Exception($"Error in script '{name}': {e.Message}", e);
                    throw ex;
                }
            }
		}

	}

}
