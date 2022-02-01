
namespace app
{
    public interface ILogic
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}