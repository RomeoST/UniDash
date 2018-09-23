using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.DAL.Repositories
{
    public class UserRepository : RepositoryBase<DutUser>, IUserRepository
    {
        public UserRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface IUserRepository : IRepository<DutUser> { }
}
