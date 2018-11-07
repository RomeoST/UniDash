using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UniDash.BLL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(DutUser userDto, string password);
        ClaimsIdentity Authenticate(string login, string password);
        Task<DutUser> FindByName(string name);
        Task<IEnumerable<UserDetails>> GetAll();
        Task SetInitialData(DutUser adminDto, List<string> roles);

        Task<bool> HasPermission(string userName,string requiredPermission);

        Task<OperationDetails> EditProfile(DutUser user);

        Task SaveUser();
    }
}
