using System;
using System.Collections.Generic;
using System.Threading;

namespace jumptest
{
    /// <summary>
    /// Thread-safe singleton session information storage class that extends Dictionary<string, string>
    /// for managing global key-value session data.
    /// </summary>
    public class SessionInfo : Dictionary<string, string>
    {
        private static readonly object _lock = new object();
        private static SessionInfo _instance = null;

        /// <summary>
        /// Gets the singleton instance of SessionInfo.
        /// </summary>
        public static SessionInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionInfo();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Private constructor to prevent direct instantiation.
        /// Use SessionInfo.Instance to get the singleton instance.
        /// </summary>
        private SessionInfo() : base()
        {
        }

        /// <summary>
        /// Private constructor to prevent direct instantiation.
        /// Use SessionInfo.Instance to get the singleton instance.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the SessionInfo can contain.</param>
        private SessionInfo(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Private constructor to prevent direct instantiation.
        /// Use SessionInfo.Instance to get the singleton instance.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new SessionInfo.</param>
        private SessionInfo(IDictionary<string, string> collection) : base(collection)
        {
        }

        /// <summary>
        /// Thread-safe getter for the value associated with the specified key.
        /// If the key doesn't exist, returns null instead of throwing an exception.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or null if the key is not found.</returns>
        public new string this[string key]
        {
            get
            {
                lock (_lock)
                {
                    return TryGetValue(key, out string value) ? value : null;
                }
            }
        }

        /// <summary>
        /// Thread-safe setter for the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(string key, string value)
        {
            lock (_lock)
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// Thread-safe method to safely get a value from the session, returning a default value if the key doesn't exist.
        /// </summary>
        /// <param name="key">The key to look up.</param>
        /// <param name="defaultValue">The default value to return if the key doesn't exist.</param>
        /// <returns>The value associated with the key, or the default value if not found.</returns>
        public string GetValue(string key, string defaultValue = null)
        {
            lock (_lock)
            {
                return TryGetValue(key, out string value) ? value : defaultValue;
            }
        }

        /// <summary>
        /// Thread-safe method to check if a key exists in the session and has a non-null, non-empty value.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if the key exists and has a value, false otherwise.</returns>
        public bool HasValue(string key)
        {
            lock (_lock)
            {
                return TryGetValue(key, out string value) && !string.IsNullOrEmpty(value);
            }
        }

        /// <summary>
        /// Thread-safe method to remove a key from the session.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>True if the key was removed, false if it didn't exist.</returns>
        public bool RemoveValue(string key)
        {
            lock (_lock)
            {
                return base.Remove(key);
            }
        }

        /// <summary>
        /// Thread-safe method to get all keys in the session.
        /// </summary>
        /// <returns>A copy of all keys in the session.</returns>
        public List<string> GetAllKeys()
        {
            lock (_lock)
            {
                return new List<string>(Keys);
            }
        }

        /// <summary>
        /// Thread-safe method to clear all values from the session.
        /// </summary>
        public void ClearAll()
        {
            lock (_lock)
            {
                base.Clear();
            }
        }

        /// <summary>
        /// Initializes the singleton instance with system information.
        /// This method should be called once during application startup.
        /// </summary>
        public static void Initialize()
        {
            var instance = Instance; // This ensures the singleton is created
            // The instance will be populated by calling Util.PopulateSessionInfo(instance)
        }
    }
}
