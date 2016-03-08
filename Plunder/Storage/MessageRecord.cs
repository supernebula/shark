using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Plunder.Storage
{
    public static class MessageRecord
    {
        private static Dictionary<string,string>  history = new Dictionary<string, string>();
        private static object _lock = new object();
        public static void Add(string key, string value)
        {
            lock (_lock)
            {
                history.Add(key, value);
            }
        }

        public static bool IsExist(string key)
        {
            lock (_lock)
            {
                return history.ContainsKey(key);
            }
        }

        public static int Count()
        {
            return history.Count;
        }
    }
}
