using WhoSol.Contracts.Enums;

namespace WhoSol.Contracts
{
    public class TaskResult<T>
    {
        public T ReturnValue { get; private set; }

        public TaskStatus Status { get; private set; }
    }
}
