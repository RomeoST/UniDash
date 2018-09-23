using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.BLL.Interfaces
{
    public interface IApplicantService
    {
        Task<OperationDetails> CreateApplicant(Applicant app);
        Task<OperationDetails> EditApplicant(Applicant app);
        Task<OperationDetails> DeleteApplicant(Applicant app);

        Applicant GetApplicantById(int id);
        Task<IEnumerable<Applicant>> GetApplicants();

        Task SaveApplicant();
    }
}
