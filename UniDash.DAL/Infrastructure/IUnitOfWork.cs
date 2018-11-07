using System.Threading.Tasks;

namespace UniDash.DAL.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
    }
}
