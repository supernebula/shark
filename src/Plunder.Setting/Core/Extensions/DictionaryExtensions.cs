using System;
using System.Collections.Generic;

namespace Plunder.Setting.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static T GetValue<T>(this IDictionary<string, object> dic, string key)
        {
            if (dic == null)
                throw new ArgumentNullException(nameof(dic));
            object valueObj = null;
            if (dic.TryGetValue(key, out valueObj))
            {
                if (valueObj is T)
                    return (T)valueObj;
            }
            return default(T);
        }
    }
}
