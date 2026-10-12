using System;

namespace jumptest
{
    /// <summary>
    /// Helper class for enum dropdown data containing ID and display string
    /// </summary>
    public class EnumHelper
    {
        public long id { get; set; }
        public string rwkString { get; set; } = string.Empty;

        public EnumHelper() { }

        public EnumHelper(long id, string rwkString)
        {
            this.id = id;
            this.rwkString = rwkString;
        }
    }
}
