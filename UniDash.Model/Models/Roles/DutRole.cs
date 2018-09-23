using Microsoft.AspNetCore.Identity;

namespace UniDash.Model.Models
{
    public class DutRole : IdentityRole
    {
        public DutRole() { }
        public string Description { get; set; }
    }
}
