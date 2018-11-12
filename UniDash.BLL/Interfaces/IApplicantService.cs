using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;
using UniDash.Model.Models.StructUniversity;

namespace UniDash.BLL.Interfaces
{
    public interface IApplicantService
    {
        Task<OperationDetails> CreateApplicant(Applicant app);
        Task<OperationDetails> EditApplicant(Applicant app);
        Task<OperationDetails> DeleteApplicant(int id);

        Applicant GetApplicantById(int id);
        IEnumerable<Applicant> GetApplicants(bool onlyName);
        Task<IEnumerable<Applicant>> GetApplicantsAsync();

        IEnumerable<Specialty> GetSpecialties();

        Task<int> SaveApplicant();
    }
}
