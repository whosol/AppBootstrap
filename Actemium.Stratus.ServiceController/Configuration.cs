using Actemium.Stratus.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Actemium.Stratus.ServiceController
{
    public class Configuration : IConfiguration
    {
        private readonly Dictionary<string, object> items = new Dictionary<string, object>();

        public void Set(string key, object value)
        {
            items[key] = value;
        }

        public T Get<T>(string key)
        {
            object output;

            items.TryGetValue(key, out output);

            return output is T ? (T)output : default(T);
        }

        public Dictionary<T1, Dictionary<T2, object>> GetModuleConfig<T1, T2>(string moduleName)
        {
            return Get<Dictionary<T1, Dictionary<T2, object>>>(moduleName);
        }
    }
}
