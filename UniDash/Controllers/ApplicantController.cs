using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.Model.Models;
using UniDash.Model.Models.StructUniversity;

namespace UniDash.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Applicant")]
    public class ApplicantController : Controller
    {
        private IApplicantService _applicantService { get; }

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("save")]
        [HttpPost]
        public async Task<OperationDetails> SaveApplicant([FromBody]Applicant model)
        {
            if (!ModelState.IsValid)
                return new OperationDetails(false, "Помилка валідації даних", ModelState.Values.Select(p => p.Errors.Select(z => z.ErrorMessage)));

            if (model.ApplicantId != 0)
                return await _applicantService.EditApplicant(model);

            //model.UserId = User.Identity.Get
            return await _applicantService.CreateApplicant(model);
        }

        [Route("remove/{id:int}")]
        [HttpDelete]
        public async Task<OperationDetails> RemoveApplicant(int id)
        {
            return await _applicantService.DeleteApplicant(id);
        }

        [Route("list")]
        [HttpGet]
        public IEnumerable<Applicant> GetList()
        {
            return _applicantService.GetApplicants(true);
        }

        [Route("select/{id:int}")]
        [HttpGet]
        public Applicant GetApplicant(int id)
        {
            return _applicantService.GetApplicantById(id);
        }

        [Route("specialties")]
        [HttpGet]
        public IEnumerable<Specialty> GetSpecialties()
        {
            return _applicantService.GetSpecialties();
        }
    }
}
