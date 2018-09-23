using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.BLL.Interfaces
{
    public interface IRoleService
    {
        Task<OperationDetails> Create(DutRole role);
        Task<OperationDetails> Edit(DutRole role);
        Task<OperationDetails> Delete(string id);
        List<DutRole> GetAll();
    }
}
