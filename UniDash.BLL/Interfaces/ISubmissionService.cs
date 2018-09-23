using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.BLL.Interfaces
{
    public interface ISubmissionService
    {
        Task<OperationDetails> Create(SubmissionDoc dto);
        Task<OperationDetails> Edit(SubmissionDoc dto);
        Task<OperationDetails> Delete(SubmissionDoc dto);

        Task SaveSubmission();
    }
}
