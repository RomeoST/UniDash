using System;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using UniDash.Model.Models;
using Ninject;

namespace UniDash.BLL.Services
{
    public class SubmissionService : ISubmissionService
    {
        IUnitOfWork DataBase { get; set; }
        public ISubmissionRepository _submissionRepository { get; set; }

        public SubmissionService(IUnitOfWork uof, ISubmissionRepository submissionRepository)
        {
            DataBase = uof;
            _submissionRepository = submissionRepository;
        }

        public async Task<OperationDetails> Create(SubmissionDoc dto)
        {
            var found = await _submissionRepository.GetAsync(p=>p.FullName == dto.FullName);
            if (found != null) return new OperationDetails(false, "Такий абітурієнт вже є у базі", "");

            _submissionRepository.Add(dto);
            await SaveSubmission();

            return new OperationDetails(true, "Документ абітурієнта створений", "");
        }

        public async Task<OperationDetails> Edit(SubmissionDoc dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationDetails> Delete(SubmissionDoc dto)
        {
            var found = await _submissionRepository.GetAsync(p=>p.Id == dto.Id);
            if (found == null) return new OperationDetails(false, "Абітурієнт не знайдений", "");

            _submissionRepository.Delete(dto);
            await SaveSubmission();

            return new OperationDetails(true, "Документ абітурієнта видалений", "");
        }

        public async Task SaveSubmission()
        {
            await DataBase.CommitAsync();
        }
    }
}
