using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;
using UniDash.DAL.Repositories;
using Ninject;
using UniDash.Model.Models.StructUniversity;

namespace UniDash.BLL.Services
{
    public class ApplicantService : IApplicantService
    {
        private IUnitOfWork _dataBase { get; }
        private IApplicantRepository _applicantRepository { get; }
        private IUStructureRepository<Specialty> _specialtyRepository { get; }

        public ApplicantService(IUnitOfWork uof, IApplicantRepository applicantRepository, IUStructureRepository<Specialty> specialtyRepository)
        {
            _dataBase = uof;
            _applicantRepository = applicantRepository;
            _specialtyRepository = specialtyRepository;
        }

        /// <summary>
        /// Створення абітурієнта
        /// </summary>
        /// <param name="applicant">Дані про абітурієнта</param>
        /// <returns></returns>
        public async Task<OperationDetails> CreateApplicant(Applicant applicant)
        {
            try
            {
                var app = await _applicantRepository.GetAsync(p => p.PhoneApplicant == applicant.PhoneApplicant ||
                                                                    (p.MailApplicant == applicant.MailApplicant && !string.IsNullOrEmpty(applicant.MailApplicant)) ||
                                                                    p.NameApplicant == applicant.NameApplicant);
                if (app != null)
                    return new OperationDetails(false, "Такий абітурієнт вже існує", new[] { $"Id - {app.ApplicantId}"});

                applicant.DateAdd = DateTime.Now;
                applicant.DateEdit = DateTime.Now;

                _applicantRepository.Add(applicant);
                await SaveApplicant();
                var newUser = await _applicantRepository.GetAsync(p => p.PhoneApplicant == applicant.PhoneApplicant);

                return new OperationDetails(true, "Абітурієнт доданий у базу!", new { applicant = newUser});
            }
            catch (Exception e)
            {
                return new OperationDetails(false, e.Message, "");
            }

        }

        /// <summary>
        /// Редагування даних абітурієнта
        /// </summary>
        /// <param name="app">Дані про абітурієнта, перевірка по id</param>
        /// <returns></returns>
        public async Task<OperationDetails> EditApplicant(Applicant app)
        {
            try
            {
                //TODO: Дописать проверки на корректность вводимых данных
                var result = _applicantRepository.GetById(app.ApplicantId);
                if (result == null)
                    return new OperationDetails(false, "Помилка", new []{ "Помилка при додаванні, повторіть спробу пізніше" });

                var checkApp = await _applicantRepository.GetAsync(p => p.ApplicantId != app.ApplicantId && (p.PhoneApplicant == app.PhoneApplicant ||
                                                                   (p.MailApplicant == app.MailApplicant && !string.IsNullOrEmpty(app.MailApplicant)) ||
                                                                   p.NameApplicant == app.NameApplicant));
                if (checkApp != null)
                    return new OperationDetails(false, "Такий абітурієнт вже існує", new[] {$"Id - {checkApp.ApplicantId}"});

                result.NameApplicant = app.NameApplicant;
                result.Address = app.Address;
                result.MailApplicant = app.MailApplicant;
                result.MarkResult = app.MarkResult;
                result.PhoneApplicant = app.PhoneApplicant;
                result.SchoolCollege = app.SchoolCollege;
                result.Speciality = app.Speciality;
                result.DateEdit = DateTime.Now;
                _applicantRepository.Update(result);

                await SaveApplicant();

                return new OperationDetails(true, "Абітурієнт збережений!", new { applicantName = app.NameApplicant });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        /// <summary>
        /// Видалення абітурієнта
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OperationDetails> DeleteApplicant(int id)
        {
            var result = _applicantRepository.GetById(id);
            if(result == null)
                return new OperationDetails(false, "Помилка", new []{ "Помилка при видалені, повторіть спробу пізніше" });

            _applicantRepository.Delete(result);

            return await SaveApplicant() == 0 ? 
                new OperationDetails(false, "Помилка", new[] { "Помилка при видалені, повторіть спробу пізніше" }) :
                new OperationDetails(true, "Абітурієнт видалений", "");
        }

        public Applicant GetApplicantById(int id) => _applicantRepository.GetById(id);

        public IEnumerable<Applicant> GetApplicants(bool onlyName)
        {
            if(!onlyName)
                return _applicantRepository.GetAll();

            return _applicantRepository.Query().Select(p => new Applicant
                {ApplicantId = p.ApplicantId, NameApplicant = p.NameApplicant});
        } 
        public async Task<IEnumerable<Applicant>> GetApplicantsAsync() => await _applicantRepository.GetAllAsync();

        public IEnumerable<Specialty> GetSpecialties()
        {
            return _specialtyRepository.GetAll();
        }

        public async Task<int> SaveApplicant()
        {
            return await _dataBase.CommitAsync();
        }
    }
}
