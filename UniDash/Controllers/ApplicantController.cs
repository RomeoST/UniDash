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
    //[Authorize]
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

            if (model.ApplicantId == 0)
                return await _applicantService.CreateApplicant(model);

            return await _applicantService.EditApplicant(model);

        }

        [Route("list")]
        [HttpGet]
        public IEnumerable<Applicant> GetList()
        {
            var a = _applicantService.GetApplicants();

            return a;
        }

        [Route("Specialties")]
        [HttpGet]
        public IEnumerable<Specialty> GetSpecialties()
        {
            return _applicantService.GetSpecialties();
        }
    }
}
