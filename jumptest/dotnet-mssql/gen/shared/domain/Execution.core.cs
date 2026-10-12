using System;
using System.ComponentModel;

namespace jumptest
{
    /// <summary>
    /// Partial Execution class containing enumerated types for execution status values
    /// </summary>
    public partial class Execution
    {
        /// <summary>
        /// Execution status enumeration based on core.exec_status table values
        /// </summary>
        public enum ExecStatus
        {
            /// <summary>
            /// Idle / no execution is currently running
            /// </summary>
            [Description("Idle")]
            Idle = 1,

            /// <summary>
            /// Execution has been dispatched
            /// </summary>
            [Description("Dispatched")]
            Dispatched = 2,

            /// <summary>
            /// Execution is currently executing
            /// </summary>
            [Description("Executing")]
            Executing = 3,

            /// <summary>
            /// Execution completed successfully
            /// </summary>
            [Description("Completed")]
            Completed = 4,

            /// <summary>
            /// Execution failed
            /// </summary>
            [Description("Failed")]
            Failed = 5,

            /// <summary>
            /// Execution was cancelled
            /// </summary>
            [Description("Cancelled")]
            Cancelled = 6,

            /// <summary>
            /// Execution timed out
            /// </summary>
            [Description("Timeout")]
            Timeout = 7,

            /// <summary>
            /// Execution was interrupted
            /// </summary>
            [Description("Interrupted")]
            Interrupted = 8,

            /// <summary>
            /// Execution is suspended
            /// </summary>
            [Description("Suspended")]
            Suspended = 9
        }

        /// <summary>
        /// Helper methods for working with execution status enumeration
        /// </summary>
        public static class ExecutionHelpers
        {
            /// <summary>
            /// Gets the description attribute value for an ExecStatus enum value
            /// </summary>
            /// <param name="status">The execution status</param>
            /// <returns>The description string</returns>
            public static string GetExecStatusDescription(ExecStatus status)
            {
                var field = status.GetType().GetField(status.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? status.ToString();
            }

            /// <summary>
            /// Parses a string description to the corresponding ExecStatus enum value
            /// </summary>
            /// <param name="description">The description string</param>
            /// <returns>The corresponding ExecStatus enum value</returns>
            public static ExecStatus ParseExecStatus(string description)
            {
                foreach (ExecStatus status in Enum.GetValues(typeof(ExecStatus)))
                {
                    if (GetExecStatusDescription(status).Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return status;
                    }
                }
                throw new ArgumentException($"Unknown execution status description: {description}");
            }
        }
    }
}
