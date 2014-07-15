
namespace Actemium.Stratus.Contracts
{
    public interface IConfiguration
    {
        void Set(string key, object value); 
        T Get<T>(string key);
    }
}
