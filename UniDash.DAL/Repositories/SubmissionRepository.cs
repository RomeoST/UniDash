using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.DAL.Repositories
{
    public class SubmissionRepository : RepositoryBase<SubmissionDoc>,ISubmissionRepository
    {
        public SubmissionRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface ISubmissionRepository : IRepository<SubmissionDoc> { }
}
