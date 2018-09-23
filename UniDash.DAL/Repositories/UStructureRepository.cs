using UniDash.DAL.Infrastructure;

namespace UniDash.DAL.Repositories
{
    public class UStructureRepository<T> : RepositoryBase<T>, IUStructureRepository<T> where T : class 
    {
        public UStructureRepository(IDataBaseFactory factory) : base(factory) { }
    }

    public interface IUStructureRepository<T> : IRepository<T> where T : class 
    { }
}
