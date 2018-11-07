using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniDash.Model.Models
{
    public class UserPosition
    {
        [ForeignKey("DutUser")]
        public int UserId { get; set; }
        [ForeignKey("Institute")]
        public int InstituteId { get; set; }
        public Institute Institute { get; set; }
        [ForeignKey("Faculty")]
        public int FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
