using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UniDash.Model.Models
{ 
    /// <summary>
    /// Клас таблиці інформації о користувачі
    /// </summary>
    public class UserDetails
    {
        [ForeignKey("DutUser")]
        public string Id { get; set; }
        public bool Enable { get; set; }

        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastVisitDate { get; set; }

        public virtual DutUser DutUser { get; set; }
        public virtual UserPosition UserPosition { get; set; }

        public UserDetails() => CreateDate = DateTime.Now;

        public string PasswordSalt;
    }
}
