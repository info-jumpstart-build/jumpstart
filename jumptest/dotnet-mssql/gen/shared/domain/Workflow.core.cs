using System;
using System.ComponentModel;

namespace jumptest
{
    /// <summary>
    /// Partial WorkflowProcess class containing enumerated types for on failure action values
    /// </summary>
    public partial class Workflow
    {
        /// <summary>
        /// Workflow type enumeration based on core.workflow_type table values
        /// </summary>
        public enum WorkflowType
        {
            /// <summary>
            /// Workflow type
            /// </summary>
            [Description("Folder")]
            Folder = 1,

            /// <summary>
            /// Process type
            /// </summary>
            [Description("Process")]
            Process = 2
        }

        /// <summary>
        /// On failure action enumeration based on core.on_failure table values
        /// </summary>
        public enum OnFailureAction
        {
            /// <summary>
            /// Stop execution on failure
            /// </summary>
            [Description("Stop")]
            Stop = 1,

            /// <summary>
            /// Continue execution on failure
            /// </summary>
            [Description("Continue")]
            Continue = 2
        }


        /// <summary>
        /// Helper methods for working with workflow type enumeration
        /// </summary>
        public static class WorkflowTypeHelpers
        {
            /// <summary>
            /// Gets the description attribute value for a WorkflowType enum value
            /// </summary>
            /// <param name="type">The workflow type</param>
            /// <returns>The description string</returns>
            public static string GetWorkflowTypeDescription(WorkflowType type)
            {
                var field = type.GetType().GetField(type.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? type.ToString();
            }

            /// <summary>
            /// Parses a string description to the corresponding WorkflowType enum value
            /// </summary>
            /// <param name="description">The description string</param>
            /// <returns>The corresponding WorkflowType enum value</returns>
            public static WorkflowType ParseWorkflowType(string description)
            {
                foreach (WorkflowType type in Enum.GetValues(typeof(WorkflowType)))
                {
                    if (GetWorkflowTypeDescription(type).Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return type;
                    }
                }
                throw new ArgumentException($"Unknown workflow type description: {description}");
            }
        }

        /// <summary>
        /// Helper methods for working with on failure action enumeration
        /// </summary>
        public static class WorkflowProcessHelpers
        {
            /// <summary>
            /// Gets the description attribute value for an OnFailureAction enum value
            /// </summary>
            /// <param name="action">The on failure action</param>
            /// <returns>The description string</returns>
            public static string GetOnFailureActionDescription(OnFailureAction action)
            {
                var field = action.GetType().GetField(action.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? action.ToString();
            }

            /// <summary>
            /// Parses a string description to the corresponding OnFailureAction enum value
            /// </summary>
            /// <param name="description">The description string</param>
            /// <returns>The corresponding OnFailureAction enum value</returns>
            public static OnFailureAction ParseOnFailureAction(string description)
            {
                foreach (OnFailureAction action in Enum.GetValues(typeof(OnFailureAction)))
                {
                    if (GetOnFailureActionDescription(action).Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return action;
                    }
                }
                throw new ArgumentException($"Unknown on failure action description: {description}");
            }
        }
    }
}

