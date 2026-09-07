using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.Json;

namespace jumptest
{
    public class BaseObject : Dictionary<string, object>
    {
        public BaseObject() { }


        public BaseObject( BaseObject t)
        {
            if (t == null) return;

            // Use reflection to copy properties with ColumnInfoAttribute
            Type currentType = this.GetType();
            Type sourceType = t.GetType();
            
            // Get all public properties with ColumnInfoAttribute from the current object
            PropertyInfo[] properties = currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            
            foreach (PropertyInfo property in properties)
            {
                // Check if property has ColumnInfoAttribute
                var labelAttribute = property.GetCustomAttribute<ColumnInfoAttribute>();
                if (labelAttribute == null) continue;
                
                // Check if property has a setter
                if (!property.CanWrite) continue;
                
                try
                {
                    // Try to get the same property from the source object
                    PropertyInfo sourceProperty = sourceType.GetProperty(property.Name);
                    if (sourceProperty != null && sourceProperty.CanRead)
                    {
                        // Get the value from the source object
                        object value = sourceProperty.GetValue(t);
                        
                        // Set the value on the current object
                        property.SetValue(this, value);
                    }
                }
                catch (Exception ex)
                {
                    // Log the error but continue with other properties
                    Console.WriteLine($"Error copying property {property.Name}: {ex.Message}");
                }
            }
        }
        protected Dictionary<string, object> _defaults = new Dictionary<string, object>();

        public string domainName {get;  set;}
        public string tableName { get;  set; }
        public string tableBaseName{ get;  set; }
        public string schemaName { get;  set; }
        public string auditTableName { get;  set;}
        public bool isAudited { get; set; } = true;
        protected List<string> rwk = new List<string>();

        public List<string> getRwk() { return rwk;  }
        public string getRwkString(bool includeId = false)
        {
            List<object> rwkValues = new();
            foreach(string c in rwk)
						{
                            if(!includeId && c.EndsWith("_id")) continue;
							rwkValues.Add( this[c] );
						}
            return String.Join( " ", rwkValues);
        }

        
        
         // Override the indexer to keep dictionary & properties in sync
        public new object this[string key]
        {
            get
            {
                object v = this.ContainsKey(key) ? base[key] : _defaults[key];

                return v;
            }
            set
            {
                // 1) Check if the new value is different to avoid recursive writes
                if (!ContainsKey(key) || base[key] == null || !base[key].Equals(value))
                {
                    // 2) Update the dictionary
                    base[key] = value;
               
                }
            }
        }
       
        public override string ToString()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                
                return JsonSerializer.Serialize(this, options);
            }
            catch (Exception ex)
            {
                // Fallback to simple format if serialization fails
                return $"{this.GetType().Name}(Serialization error: {ex.Message})";
            }
        }

        protected virtual void Initialize()
        {}
    }
}
