using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading;
 
#pragma warning disable CS8604 // Possible null reference argument
#pragma warning disable CS8620 // Argument of type ... cannot be used for parameter of type ...
#pragma warning disable CS8618 // Non-nullable field is uninitialized

namespace jumptest 
{

    /// <summary>
    /// Externalized callback initializer for Proxy interceptors
    /// </summary>
    public static class ProxyCallbackInitializer
    {
        /// <summary>
        /// Initializes the default before/after action callbacks for Proxy
        /// </summary>
        public static void Initialize()
        {
            // Logging before action
            ProxyAction.AddBeforeAction((domainObj, method, args) =>
            {
                Logger.Info($"Invoking {domainObj}.{method.Name}."/* with arguments: {string.Join(", ", args)}"*/);
            });

            // Authorization before action
            ProxyAction.AddBeforeAction((domainObj, method, args) =>
            {
                Logger.Debug($"Checking {OpRoleMemberLogic.CurrentUser} authorization for {domainObj}.{method.Name}."/*" with arguments: {string.Join(", ", args)}"*/);

                bool authorized = OpRoleMemberLogic.Authorized(domainObj, method.Name);

                if (authorized)
                {
                    Logger.Debug($"{OpRoleMemberLogic.CurrentUser} is authorized for {domainObj}.{method.Name}."/* with arguments: {string.Join(", ", args)}"*/);
                } 
                else
                { 
                    throw new Exception($"Principal {OpRoleMemberLogic.CurrentUser} is not authorized for {domainObj}.{method.Name}.");
                }
            });

            // Pre-event service before action
            ProxyAction.AddBeforeAction((domainObj, method, args) =>
            {
                Logger.Info($"invoking pre event service for {domainObj}.{method.Name}." /*with arguments: {string.Join(", ", args)}"*/);

                EventContext ctx = new EventContext("pre", domainObj, method.Name, args);
                EventServiceLogic.Invoke(ctx); 

                if (ctx.ScriptException != null) throw new Exception($"Error in pre {domainObj}.{method.Name} event", ctx.ScriptException);
            });

            // Logging after action
            ProxyAction.AddAfterAction((domainObj, method, result, args) =>
            {
                Logger.Info($"After invoking {method.Name}."/* with arguments: {string.Join(", ", args)}"*/);
            });

            // Post-event service after action
            ProxyAction.AddAfterAction((domainObj, method, result, args) =>
            {
                Logger.Info($"invoking post event service for or {domainObj}.{method.Name}." /* with arguments: {string.Join(", ", args)}"*/);

                EventContext ctx = new EventContext("post", domainObj, method.Name, args, result);
                EventServiceLogic.Invoke(ctx); 
               
                if (ctx.ScriptException != null) throw new Exception($"Error in post {domainObj}.{method.Name} event", ctx.ScriptException);
            });
        }
    }

    /// <summary>
    /// Static class for managing before/after actions for Proxy interceptors
    /// </summary>
    public static class ProxyAction
    {
        // Static thread-safe collections for before/after actions
        // Callbacks now include DomainObj as the first parameter
        private static readonly ConcurrentBag<Action<string, MethodInfo, object[]>> BeforeActions = new();
        private static readonly ConcurrentBag<Action<string, MethodInfo, object, object[]>> AfterActions = new();

        /// <summary>
        /// Gets the list of before actions (thread-safe)
        /// </summary>
        public static IEnumerable<Action<string, MethodInfo, object[]>> GetBeforeActions()
        {
            return BeforeActions;
        }

        /// <summary>
        /// Gets the list of after actions (thread-safe)
        /// </summary>
        public static IEnumerable<Action<string, MethodInfo, object, object[]>> GetAfterActions()
        {
            return AfterActions;
        }

        /// <summary>
        /// Adds a before action to be executed before method invocation (thread-safe, static)
        /// </summary>
        /// <param name="action">Action that receives DomainObj, MethodInfo, and arguments</param>
        public static void AddBeforeAction(Action<string, MethodInfo, object[]> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            BeforeActions.Add(action);
        }

        /// <summary>
        /// Adds an after action to be executed after method invocation (thread-safe, static)
        /// </summary>
        /// <param name="action">Action that receives DomainObj, MethodInfo, result, and arguments</param>
        public static void AddAfterAction(Action<string, MethodInfo, object, object[]> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            AfterActions.Add(action);
        }
    }

    public class Proxy<T> : DispatchProxy 
    {   
        public T? Target { get; set; } 
        protected string _domainObj = string.Empty;
        public string DomainObj 
        { 
            get { return _domainObj; }
            set { _domainObj = value; }
        }
        
        // Static flag to ensure Initialize is only called once per program session
        private static int _initialized = 0;
        private static readonly object _initializeLock = new object();

        
        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null) return null;

            // Iterate over thread-safe collection, passing DomainObj as first parameter
            foreach (var action in ProxyAction.GetBeforeActions())
            {
               if (action != null) action.Invoke(DomainObj, targetMethod, args ?? Array.Empty<object>());
            }

            var result = targetMethod.Invoke(Target, args);

            // Iterate over thread-safe collection, passing DomainObj as first parameter
            foreach (var action in ProxyAction.GetAfterActions())
            {
                if (action != null) action.Invoke(DomainObj, targetMethod, result, args ?? Array.Empty<object>());
            }


            return result;
        }

        /// <summary>
        /// Initializes the proxy with default before/after actions.
        /// This method is guaranteed to execute only once per program session (thread-safe).
        /// </summary>
        public virtual void Initialize()
        {
            // Use Interlocked to ensure only one thread can set the flag
            if (Interlocked.CompareExchange(ref _initialized, 1, 0) != 0)
            {
                // Already initialized, return immediately
                return;
            }

            // Double-check lock pattern for additional safety
            lock (_initializeLock)
            {
                // Check again inside lock to be absolutely sure
                if (_initialized == 2)
                {
                    return;
                }

                // Perform initialization using externalized callback initializer
                ProxyCallbackInitializer.Initialize();
                
                // Mark as fully initialized
                Interlocked.Exchange(ref _initialized, 2);
            }
        }
      

    }


}
#pragma warning restore CS8604 // Possible null reference argument
#pragma warning restore CS8620 // Argument of type ... cannot be used for parameter of type ...
#pragma warning restore CS8618 // Non-nullable field is uninitialized
