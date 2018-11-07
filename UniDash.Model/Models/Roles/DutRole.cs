using Microsoft.AspNetCore.Identity;

namespace UniDash.Model.Models
{
    public class DutRole : IdentityRole
    {
        public DutRole() { }
        public DutRole(string name) : base(name) { }
        public string Description { get; set; }
    }
}
