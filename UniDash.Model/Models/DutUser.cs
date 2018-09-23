using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace UniDash.Model.Models
{
    public class DutUser : IdentityUser
    {
        public virtual UserDetails UserDetails { get; set; }
    }
}
