
using System.Collections.Generic;
namespace WhoSol.Contracts
{
    public interface IConfiguration
    {
        void Set(string key, object value);
        T Get<T>(string key);
        Dictionary<T1, Dictionary<T2, object>> GetModuleConfig<T1, T2>(string moduleName);
    }
}
