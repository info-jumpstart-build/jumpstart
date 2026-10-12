using System;
using System.Text.Json;
using System.Reflection;
using jumptest;

namespace jumptest
{
    /// <summary>
    /// Helper class for converting JSON tokens into proper property values
    /// to handle type conversion issues when data crosses the API boundary
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Converts a JSON element to the appropriate .NET type based on the target property type
        /// </summary>
        /// <param name="jsonElement">The JSON element to convert</param>
        /// <param name="targetType">The target .NET type</param>
        /// <returns>The converted value</returns>
        public static object ConvertJsonElement(JsonElement jsonElement, Type targetType)
        {
            if (jsonElement.ValueKind == JsonValueKind.Null)
                return null;

            try
            {
                // Handle nullable types
                Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

                switch (underlyingType.Name.ToLower())
                {
                    case "int64":
                    case "long":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetInt64();
                        return Convert.ToInt64(jsonElement.GetString());

                    case "int32":
                    case "int":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetInt32();
                        return Convert.ToInt32(jsonElement.GetString());

                    case "int16":
                    case "short":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetInt16();
                        return Convert.ToInt16(jsonElement.GetString());

                    case "decimal":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetDecimal();
                        return Convert.ToDecimal(jsonElement.GetString());

                    case "double":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetDouble();
                        return Convert.ToDouble(jsonElement.GetString());

                    case "single":
                    case "float":
                        if (jsonElement.ValueKind == JsonValueKind.Number)
                            return jsonElement.GetSingle();
                        return Convert.ToSingle(jsonElement.GetString());

                    case "string":
                        return jsonElement.GetString();

                    case "boolean":
                    case "bool":
                        if (jsonElement.ValueKind == JsonValueKind.True || jsonElement.ValueKind == JsonValueKind.False)
                            return jsonElement.GetBoolean();
                        return Convert.ToBoolean(jsonElement.GetString());

                    case "datetime":
                        if (jsonElement.ValueKind == JsonValueKind.String)
                            return DateTime.Parse(jsonElement.GetString());
                        return jsonElement.GetDateTime();

                    case "guid":
                        if (jsonElement.ValueKind == JsonValueKind.String)
                            return Guid.Parse(jsonElement.GetString());
                        return jsonElement.GetGuid();

                    default:
                        // For other types, try to use the JSON element's built-in conversion
                        return Convert.ChangeType(jsonElement.GetRawText(), underlyingType);
                }
            }
            catch (Exception ex)
            {
                Logger.Warning($"JSON conversion failed for type {targetType.Name}: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Processes a BaseObject and converts any JsonElement values to their proper types
        /// </summary>
        /// <param name="baseObject">The BaseObject to process</param>
        public static void ProcessJsonElements(BaseObject baseObject)
        {
            if (baseObject == null) return;

            Type objectType = baseObject.GetType();
            PropertyInfo[] properties = objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties)
            {
                // Only process properties that have a ColumnInfoAttribute
                var labelAttribute = property.GetCustomAttribute<ColumnInfoAttribute>();
                if (labelAttribute == null) continue;

                if (baseObject.ContainsKey(property.Name))
                {
                    object value = baseObject[property.Name];
                    
                    if (value is JsonElement jsonElement)
                    {
                        object convertedValue = ConvertJsonElement(jsonElement, property.PropertyType);
                        baseObject[property.Name] = convertedValue;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a properly typed object from JSON data
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <param name="jsonString">The JSON string</param>
        /// <returns>The deserialized and processed object</returns>
        public static T FromJson<T>(string jsonString) where T : BaseObject, new()
        {
            if (string.IsNullOrEmpty(jsonString))
                return null;

            try
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var baseObject = JsonSerializer.Deserialize<T>(jsonString, options);
                ProcessJsonElements(baseObject);
                return baseObject;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to deserialize JSON: {ex.Message}");
                return null;
            }
        }
    }
}
