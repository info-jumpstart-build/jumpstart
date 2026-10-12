using System.Reflection;
using System.Linq;

namespace jumptest
{
    /// <summary>
    /// Helper class for reflection-based operations on domain objects
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Gets all properties that should be displayed in UI (has ColumnInfoAttribute and doesn't end with _id)
        /// </summary>
        /// <typeparam name="T">Type to get properties from</typeparam>
        /// <returns>Array of PropertyInfo for displayable properties</returns>
        public static PropertyInfo[] GetDisplayProperties<T>()
        {
            var type = typeof(T);
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.CanRead && 
                                  p.GetCustomAttribute<ColumnInfoAttribute>() != null &&
                                  !p.Name.EndsWith("_id"))
                      .OrderBy(p => GetPropertyOrder(p))
                      .ToArray();
        }

      public static PropertyInfo[] GetEditFormProperties<T>()
        {
            var type = typeof(T);
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.CanRead && 
                                  p.GetCustomAttribute<ColumnInfoAttribute>() != null
                                  )
                      .OrderBy(p => GetPropertyOrder(p))
                      .ToArray();
        }

        /// <summary>
        /// Gets all properties that should be displayed in UI (has ColumnInfoAttribute and doesn't end with _id)
        /// </summary>
        /// <param name="type">Type to get properties from</param>
        /// <returns>Array of PropertyInfo for displayable properties</returns>
        public static PropertyInfo[] GetDisplayProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.CanRead && 
                                  p.GetCustomAttribute<ColumnInfoAttribute>() != null &&
                                  !p.Name.EndsWith("_id"))
                      .OrderBy(p => GetPropertyOrder(p))
                      .ToArray();
        }

        /// <summary>
        /// Gets the display label for a property
        /// </summary>
        /// <param name="property">PropertyInfo to get label for</param>
        /// <returns>Label from ColumnInfoAttribute or formatted property name</returns>
        public static string GetPropertyLabel(PropertyInfo property)
        {
            // Check for Label attribute first
            var labelAttr = property.GetCustomAttribute<ColumnInfoAttribute>();
            if (labelAttr != null)
            {
                return labelAttr.Label;
            }

            // Fallback to property name with proper formatting
            return FormatPropertyName(property.Name);
        }

        /// <summary>
        /// Gets the value of a property from an object
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value or null if error occurs</returns>
        public static object GetPropertyValue(object obj, PropertyInfo property)
        {
            try
            {
                return property.GetValue(obj);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a string
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as string or empty string if error occurs</returns>
        public static string GetPropertyValueAsString(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                return value?.ToString() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as an int
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as int or 0 if error occurs</returns>
        public static int GetPropertyValueAsInt(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return 0;
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable int
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as int? or null if error occurs</returns>
        public static int? GetPropertyValueAsNullableInt(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a long
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as long or 0 if error occurs</returns>
        public static long GetPropertyValueAsLong(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return 0;
                return Convert.ToInt64(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable long
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as long? or null if error occurs</returns>
        public static long? GetPropertyValueAsNullableLong(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToInt64(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a decimal
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as decimal or 0 if error occurs</returns>
        public static decimal GetPropertyValueAsDecimal(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return 0;
                return Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable decimal
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as decimal? or null if error occurs</returns>
        public static decimal? GetPropertyValueAsNullableDecimal(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToDecimal(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a double
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as double or 0 if error occurs</returns>
        public static double GetPropertyValueAsDouble(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return 0;
                return Convert.ToDouble(value);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable double
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as double? or null if error occurs</returns>
        public static double? GetPropertyValueAsNullableDouble(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToDouble(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a bool
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as bool or false if error occurs</returns>
        public static bool GetPropertyValueAsBool(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return false;
                return Convert.ToBoolean(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable bool
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as bool? or null if error occurs</returns>
        public static bool? GetPropertyValueAsNullableBool(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToBoolean(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a DateTime
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as DateTime or DateTime.MinValue if error occurs</returns>
        public static DateTime GetPropertyValueAsDateTime(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return DateTime.MinValue;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable DateTime
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as DateTime? or null if error occurs</returns>
        public static DateTime? GetPropertyValueAsNullableDateTime(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a DateOnly
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as DateOnly or DateOnly.MinValue if error occurs</returns>
        public static DateOnly GetPropertyValueAsDateOnly(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return DateOnly.MinValue;
                if (value is DateOnly dateOnly) return dateOnly;
                if (value is DateTime dateTime) return DateOnly.FromDateTime(dateTime);
                return DateOnly.FromDateTime(Convert.ToDateTime(value));
            }
            catch
            {
                return DateOnly.MinValue;
            }
        }

        /// <summary>
        /// Gets the value of a property from an object as a nullable DateOnly
        /// </summary>
        /// <param name="obj">Object to get property value from</param>
        /// <param name="property">PropertyInfo to get value for</param>
        /// <returns>Property value as DateOnly? or null if error occurs</returns>
        public static DateOnly? GetPropertyValueAsNullableDateOnly(object obj, PropertyInfo property)
        {
            try
            {
                var value = property.GetValue(obj);
                if (value == null) return null;
                if (value is DateOnly dateOnly) return dateOnly;
                if (value is DateTime dateTime) return DateOnly.FromDateTime(dateTime);
                return DateOnly.FromDateTime(Convert.ToDateTime(value));
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the display order for a property
        /// </summary>
        /// <param name="property">PropertyInfo to get order for</param>
        /// <returns>Order value for property sorting</returns>
        public static int GetPropertyOrder(PropertyInfo property)
        {
            // Check for Label attribute with order
            /*
            var labelAttr = property.GetCustomAttribute<ColumnInfoAttribute>();
            if (labelAttr != null && labelAttr.Order > 0)
            {
                return labelAttr.Order;
            }
            */
            
            // Default ordering: id first, then alphabetical
            if (property.Name.ToLower() == "id")
                return 0;
            
            return 1000; // Default order for properties without explicit ordering
        }

        /// <summary>
        /// Formats a property name for display (converts camelCase/PascalCase to Title Case)
        /// </summary>
        /// <param name="propertyName">Property name to format</param>
        /// <returns>Formatted property name</returns>
        public static string FormatPropertyName(string propertyName)
        {
            // Convert camelCase/PascalCase to Title Case
            return System.Text.RegularExpressions.Regex.Replace(propertyName, "([a-z])([A-Z])", "$1 $2");
        }

        /// <summary>
        /// Gets all properties that have a ColumnInfoAttribute (including those ending with _id)
        /// </summary>
        /// <typeparam name="T">Type to get properties from</typeparam>
        /// <returns>Array of PropertyInfo for all labeled properties</returns>
        public static PropertyInfo[] GetAllLabeledProperties<T>()
        {
            var type = typeof(T);
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.CanRead && 
                                  p.GetCustomAttribute<ColumnInfoAttribute>() != null)
                      .OrderBy(p => GetPropertyOrder(p))
                      .ToArray();
        }

        /// <summary>
        /// Gets all properties that have a ColumnInfoAttribute (including those ending with _id)
        /// </summary>
        /// <param name="type">Type to get properties from</param>
        /// <returns>Array of PropertyInfo for all labeled properties</returns>
        public static PropertyInfo[] GetAllLabeledProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                      .Where(p => p.CanRead && 
                                  p.GetCustomAttribute<ColumnInfoAttribute>() != null)
                      .OrderBy(p => GetPropertyOrder(p))
                      .ToArray();
        }

        /// <summary>
        /// Checks whether a property is one of the global/audit columns
        /// (is_active, created_by, last_updated, last_updated_by, txn_id --
        /// see templates/core/global.csv) added to every non-view table.
        /// These are managed by the persistence layer, so Edit pages render
        /// them read-only instead of as editable inputs.
        /// </summary>
        /// <param name="property">PropertyInfo to check</param>
        /// <returns>True if the property's ColumnInfoAttribute.isGlobal is set</returns>
        public static bool IsGlobalProperty(PropertyInfo property)
        {
            var columnInfo = property.GetCustomAttribute<ColumnInfoAttribute>();
            return columnInfo != null && columnInfo.isGlobal;
        }

        /// <summary>
        /// Checks if a property has a ColumnInfoAttribute
        /// </summary>
        /// <param name="property">PropertyInfo to check</param>
        /// <returns>True if property has ColumnInfoAttribute</returns>
        public static bool HasColumnInfoAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<ColumnInfoAttribute>() != null;
        }

        /// <summary>
        /// Gets the ColumnInfoAttribute from a property
        /// </summary>
        /// <param name="property">PropertyInfo to get attribute from</param>
        /// <returns>ColumnInfoAttribute or null if not found</returns>
        public static ColumnInfoAttribute GetColumnInfoAttribute(PropertyInfo property)
        {
            return property.GetCustomAttribute<ColumnInfoAttribute>();
        }
    }
}
