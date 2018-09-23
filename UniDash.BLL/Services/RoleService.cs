using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;
using Ninject;

namespace UniDash.BLL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork _dataBase { get; set; }
        RoleManager<DutRole> _roleManager { get; set; }

        public RoleService(IUnitOfWork dataBase ,RoleManager<DutRole> roleManager)
        {
            _dataBase = dataBase;
            _roleManager = roleManager;
        }

        public async Task<OperationDetails> Create(DutRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded 
                ? new OperationDetails(true, "Додан новий тип користувача", "") 
                : new OperationDetails(false, result.Errors.Aggregate("", (current, resultError) => current + (resultError + ",")), "Role");
        }

        public async Task<OperationDetails> Edit(DutRole model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.UpdateAsync(model);
                return result.Succeeded 
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "") 
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public async Task<OperationDetails> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded
                    ? new OperationDetails(true, "Оновлення пройшло успішно", "")
                    : new OperationDetails(false, result.Errors.Aggregate("", (s, s1) => s + (s1 + ",")), "Role");
            }
            return new OperationDetails(false, "Тип користувача не знайден", "");
        }

        public List<DutRole> GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }
    }
}
