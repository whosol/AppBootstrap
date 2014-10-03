namespace WhoSol.Contracts
{
    public interface IParser<T>
    {
        T Parse(string filename);
    }
}
