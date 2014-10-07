namespace WhoSol.Contracts
{
    public interface IFactory<T>
    {
        IParser<T> Get(string data);
    }
}
