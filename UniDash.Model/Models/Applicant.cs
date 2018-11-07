using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniDash.Model.Models
{
    /// <summary>
    /// Класс таблиці роботи з абітурієнтами
    /// </summary>
    public class Applicant
    {
        public int ApplicantId { get; set; }
        [ForeignKey("DutUser")]
        public string UserId { get; set; }
        [Required]
        public string NameFound { get; set; }
        [Required]
        public string NameApplicant { get; set; }
        [Required]
        [EmailAddress]
        public string MailApplicant { get; set; }
        [Required]
        [StringLength(9, ErrorMessage = "Невірно вказаний телефон")]
        public string PhoneApplicant { get; set; }
        [Required]
        public string SchoolCollege { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Speciality { get; set; }
        public string MarkResult { get; set; }
        [BindNever]
        public DateTime DateEdit { get; set; }
        [BindNever]
        public DateTime DateAdd { get; set; }

        public virtual DutUser DutUser { get; set; }

        public Applicant() => DateAdd = DateTime.Now;
    }
}
