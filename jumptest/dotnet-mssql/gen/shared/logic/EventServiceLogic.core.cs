
using System;


namespace jumptest
{
    

    public class EventContext : ScriptContext
    {
        public string EventType { get; private set;}
				public string ObjectName {get; private set;}
				public string MethodName {get; private set;} 
				public object? MethodResult { get; private set;}

        public EventContext(string eventType, string objectName, string methodName, object[] args) 
        {
		        Initialize (eventType,  objectName,  methodName, args);
        }

        public EventContext(string eventType, string objectName, string methodName, object[] args, object methodResult) 
        {
				this.MethodResult = methodResult;

		        Initialize (eventType,  objectName,  methodName, args);
        }

        public void Initialize(string eventType, string objectName, string methodName, object[] args)
				{
                    Initialize();

                    EventType = eventType;
					ObjectName = objectName;
					MethodName = methodName;

                    Args.Add("eventtype", eventType);
                    Args.Add("objectname", objectName);
                    Args.Add("methodname", methodName);
                    Args.Add("args", args);

                    foreach (var a in args )
                    {
                        if (a is BaseObject) 
                        {
                            this.Transaction = a as BaseObject;
                            break;
                        }
                    } 
				}
    }

    public partial class EventServiceLogic
    {

        protected static Dictionary< string, List<Script> > scriptCache = new();
        protected static object cacheLock = new object();

        public static void Invoke( EventContext eventContext )
        {
            string[] keyArray = new  string[] { eventContext.ObjectName, eventContext.MethodName};

            string key = string.Join(":", keyArray);

            List<Script> scripts = new();

            lock(cacheLock)
            {
                if (scriptCache.ContainsKey(key))
                {
                    scripts = scriptCache[key];
                }
                else
                {
                    string sql = @"SELECT s.id, s.name, s.source, s.is_active, s.created_by, s.last_updated, s.last_updated_by, s.txn_id
    FROM core.event_service e
inner join core.script s on
    s.id=e.script_id 
    and s.is_active=1
where
e.is_active=1 and
e.event_type='^(eventtype)' and
'^(objectname)' like e.objectname_filter and
'^(methodname)' like e.methodname_filter";

                    BaseObject filter = new();
                    filter["eventtype"] = eventContext.EventType;
                    filter["objectname"] = eventContext.ObjectName;
                    filter["methodname"] = eventContext.MethodName;
                    

                    void  scriptCallback(System.Data.Common.DbDataReader rdr)
                    {
                        
                        Script s = new Script();

                        DBPersist.autoAssign(rdr, s);

                        scripts.Add(s);
                    };
                    
                    DBPersist.select( scriptCallback, sql, filter);

                    scriptCache[key] = scripts;
                }
            } 

            if (scripts.Count > 0)
            {
                foreach ( Script s in scripts)
                {   // TODO - add a queued cache to contain a fixed number of script hosts
                    ScriptHost sh = new ScriptHost();
                    sh.Invoke<EventContext>(eventContext, s);
                }
            }


            
        }
        
    }
}

