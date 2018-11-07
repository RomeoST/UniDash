using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;
using UniDash.Model.Models.StructUniversity;

namespace UniDash.BLL.Interfaces
{
    public interface IUStructService
    {
        Task<OperationDetails> Create(UStructBase structure, TypeStructUniversity type);
        Task<OperationDetails> Delete(UStructBase structure, TypeStructUniversity type);
        Task<UStructBase> GetById(int id, TypeStructUniversity type);
        Task<IEnumerable<UStructBase>> GetAll(TypeStructUniversity type);

        Task<IEnumerable<Specialty>> GetSpecialty();

        Task SaveStructure();
    }
}
