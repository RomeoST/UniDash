using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniDash.DAL.Infrastructure;
using UniDash.Model.Models;

namespace UniDash.DAL.Repositories
{
    /// <summary>
    /// Клас логіки роботи з паравами доступу до методів
    /// </summary>
    public class PermissionRepository : RepositoryBase<PermissionRoles>, IPermissionRepository
    {
        public PermissionRepository(IDataBaseFactory factory) : base(factory)
        {
        }

        public async Task<IEnumerable<DutRole>> GetPermissionRoles(string controller, string action)
        {
            var roles = await GetManyAsync(p => p.Controller.Name == controller && p.Action.Name == action);
            return roles.Select(p=>p.DutRole);
        }
    }

    public class ControllerRepository : RepositoryBase<TController>, IControllerRepository
    {
        public ControllerRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    public class ActionRepository : RepositoryBase<TAction>, IActionRepository
    {
        public ActionRepository(IDataBaseFactory factory) : base(factory)
        {
        }
    }

    #region Interfaces

    public interface IPermissionRepository : IRepository<PermissionRoles>
    {
        Task<IEnumerable<DutRole>> GetPermissionRoles(string controller, string action);
    }
    public interface IControllerRepository : IRepository<TController> { }
    public interface IActionRepository : IRepository<TAction> { }
    #endregion

}
