using System;
using System.ComponentModel;

namespace jumptest
{
    /// <summary>
    /// Partial ServerNode class containing enumerated types for server node status values
    /// </summary>
    public partial class ServerNode
    {
        /// <summary>
        /// Server node type enumeration based on core.server_node_type table values
        /// </summary>
        public enum ServerNodeType
        {
            /// <summary>
            /// Agent server node
            /// </summary>
            [Description("Agent")]
            Agent = 1,

            /// <summary>
            /// Scheduler server node
            /// </summary>
            [Description("Scheduler")]
            Scheduler = 2,

            /// <summary>
            /// API server node
            /// </summary>
            [Description("ApiServer")]
            ApiServer = 3
        }

        /// <summary>
        /// Server node status enumeration based on core.server_node_status table values
        /// </summary>
        public enum ServerNodeStatus
        {
            /// <summary>
            /// Server node is initializing
            /// </summary>
            [Description("Initializing")]
            Initializing = 1,

            /// <summary>
            /// Server node is online and available
            /// </summary>
            [Description("Online")]
            Online = 2,

            /// <summary>
            /// Server node is busy processing tasks
            /// </summary>
            [Description("Busy")]
            Busy = 3,

            /// <summary>
            /// Server node is offline
            /// </summary>
            [Description("Offline")]
            Offline = 4
        }

        /// <summary>
        /// Helper methods for working with server node status enumeration
        /// </summary>
        public static class ServerNodeHelpers
        {
            /// <summary>
            /// Gets the description attribute value for an ServerNodeStatus enum value
            /// </summary>
            /// <param name="status">The server node status</param>
            /// <returns>The description string</returns>
            public static string GetServerNodeStatusDescription(ServerNodeStatus status)
            {
                var field = status.GetType().GetField(status.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                return attribute?.Description ?? status.ToString();
            }

            /// <summary>
            /// Parses a string description to the corresponding ServerNodeStatus enum value
            /// </summary>
            /// <param name="description">The description string</param>
            /// <returns>The corresponding ServerNodeStatus enum value</returns>
            public static ServerNodeStatus ParseServerNodeStatus(string description)
            {
                foreach (ServerNodeStatus status in Enum.GetValues(typeof(ServerNodeStatus)))
                {
                    if (GetServerNodeStatusDescription(status).Equals(description, StringComparison.OrdinalIgnoreCase))
                    {
                        return status;
                    }
                }
                throw new ArgumentException($"Unknown server node status description: {description}");
            }
        }
    }
}