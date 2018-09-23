using System.Threading.Tasks;

namespace UniDash.DAL.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();
    }
}
