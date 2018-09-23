using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.DAL.Repositories
{
    /// <summary>
    /// Клас логіки роботи з детальними даними користувача
    /// </summary>
    public class UserDetailsRepository : RepositoryBase<UserDetails>, IUserDetailsRepository
    {
        public UserDetailsRepository(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IUserDetailsRepository : IRepository<UserDetails> { }
}
