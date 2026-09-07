using System;
using System.ComponentModel;

namespace jumptest
{
    /// <summary>
    /// Partial Script class containing enumerated types for script type values
    /// </summary>
    public partial class Script
    {
        /// <summary>
        /// Script type enumeration based on core.script_type table values
        /// </summary>
        public enum ScriptType
        {
            /// <summary>
            /// C# script
            /// </summary>
            [Description("CSharp")]
            CSharp = 1,

            /// <summary>
            /// PowerShell script
            /// </summary>
            [Description("PowerShell")]
            PowerShell = 2,

            /// <summary>
            /// Python script
            /// </summary>
            [Description("Python")]
            Python = 3
        }

        /// <summary>
        /// Helper methods for working with script type enumeration
        /// </summary>
        public static class ScriptHelpers
        {
            /// <summary>
            /// Gets the description attribute value for a ScriptType enum value
            /// </summary>
            /// <param name="scriptType">The script type</param>
            /// <returns>The description string</returns>
            public static string GetScriptTypeDescription(ScriptType scriptType)
            {
                var field = scriptType.GetType().GetField(scriptType.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? scriptType.ToString();
            }

            /// <summary>
            /// Parses a string description to the corresponding ScriptType enum value
            /// </summary>
            /// <param name="description">The description string</param>
            /// <returns>The corresponding ScriptType enum value</returns>
            public static ScriptType ParseScriptType(string description)
            {
                foreach (ScriptType scriptType in Enum.GetValues(typeof(ScriptType)))
                {
                    if (GetScriptTypeDescription(scriptType).Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return scriptType;
                    }
                }
                throw new ArgumentException($"Unknown script type description: {description}");
            }
        }
    }
}

