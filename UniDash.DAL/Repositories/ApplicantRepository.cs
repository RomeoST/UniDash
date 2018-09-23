using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.DAL.Repositories
{
    /// <summary>
    /// Клас логіки системи абітурієнтів
    /// </summary>
    public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public interface IApplicantRepository : IRepository<Applicant> { }
}
