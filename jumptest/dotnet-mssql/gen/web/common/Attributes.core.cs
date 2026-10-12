using System;
using System.Reflection;

namespace jumptest
{

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ClassInfoAttribute : Attribute
	{
	    public string Label { get; }

	    public ClassInfoAttribute(string label)
	    {
	        Label = label;
	    }

	    public static string GetClassLabel<T>()
	    {
	        var ClassInfoAttribute = typeof(T).GetCustomAttribute<ClassInfoAttribute>();
	        return ClassInfoAttribute?.Label ?? $"No Label found for class '{typeof(T).Name}'";
	    }

	  
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
	public class ColumnInfoAttribute : Attribute
	{
	    public string Label { get; set; }
		public string fkObject { get; set; }
		public string fkType { get; set; }
		public string fkTable { get; set; }
		public string fkVar { get; set; }
		// True for the global/audit columns added to every table (is_active,
		// created_by, last_updated, last_updated_by, txn_id -- see
		// templates/core/global.csv and MetaAttribute.IsGlobal()). These are
		// managed by the persistence layer, so Edit pages render them
		// read-only instead of as editable inputs.
		public bool isGlobal { get; set; }

	    public ColumnInfoAttribute(string label, string fkObject = "", string fkType = "", string fkTable = "", string fkVar = "", bool isGlobal = false)
	    {
	        this.Label = label;
			this.fkObject = fkObject;
			this.fkType = fkType;
			this.fkTable = fkTable;
			this.fkVar = fkVar;
			this.isGlobal = isGlobal;
	    }

	  

	    public static string GetPropertyLabel<T>(string propertyName)
	    {
	        var property = typeof(T).GetProperty(propertyName);
	        if (property != null)
	        {
	            var ColumnInfoAttribute = property.GetCustomAttribute<ColumnInfoAttribute>();
	            return ColumnInfoAttribute?.Label ?? $"No Label found for '{propertyName}'";
	        }
	        return $"Property '{propertyName}' not found";
	    }
	}
}
