using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using UniDash.Model.Models;
using Microsoft.AspNetCore.Identity;

namespace UniDash.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _dataBase { get;}
        private IUserDetailsRepository _userDetailsRepository { get;}
        private IPermissionRepository _permissionRepository { get;}
        private UserManager<DutUser> _userManager { get;}
        private RoleManager<DutRole> _roleManager { get;}

        public UserService(IUnitOfWork dataBase, IUserDetailsRepository userDetailsRepository,
            IPermissionRepository permissionRepository, UserManager<DutUser> userManager, RoleManager<DutRole> roleManager)
        {
            _dataBase = dataBase;
            _userDetailsRepository = userDetailsRepository;
            _permissionRepository = permissionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationDetails> Create(DutUser userDto, string password)
        {
            var user = await _userManager.FindByEmailAsync(userDto.Email);
            if (user != null) return new OperationDetails(false, "Користувач з таким email вже існує", "Email");

            var result = await _userManager.CreateAsync(userDto, password);
            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault().Description, "");

            // Добавить роль
            await _userManager.AddToRoleAsync(userDto, "user");

            // Создание профиля клиента
            UserDetails userDetails = new UserDetails {Id = userDto.Id, FullName = userDto.UserDetails.FullName};
            _userDetailsRepository.Add(userDetails);
            await SaveUser();

            return new OperationDetails(true, "Реєстрація успішно пройдена","");

        }

        public ClaimsIdentity Authenticate(string login, string password)
        {
            var user = _userManager.Users.SingleOrDefault(p => p.UserName == login);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.UserDetails.PasswordSalt))
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };

            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<DutUser> FindByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            return user;
        }

        public async Task<IEnumerable<UserDetails>> GetAll()
        {
            return await _userDetailsRepository.GetAllAsync();
        }

        // Инициализация БД  (начальная)
        public async Task SetInitialData(DutUser adminDto, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null) continue;

                role = new DutRole() {Name = roleName};
                await _roleManager.CreateAsync(role);
            }
        }

        /// <summary>
        /// Проверить права доступа
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="requiredPermission">/<controller-action/></param>
        /// <returns></returns>
        public async Task<bool> HasPermission(string userName, string requiredPermission)
        {
            try
            {
                var required = requiredPermission.Split('-');
                var roles = await _permissionRepository.GetPermissionRoles(required[0], required[1]);

                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                    return false;

                return roles.Select(p => _userManager.IsInRoleAsync(user, p.Name)).Any();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<OperationDetails> EditProfile(DutUser user)
        {
            var curus = await _userManager.FindByIdAsync(user.Id);
            if(curus == null)
                return new OperationDetails(false, "Користувач не знайдений","");
            curus.UserDetails.FullName = user.UserDetails.FullName;
            curus.Email = user.Email;
            curus.PhoneNumber = user.PhoneNumber;

            await SaveUser();
            return new OperationDetails(true,"Данні збережені","");
        }

        public async Task SaveUser()
        {
            await _dataBase.CommitAsync();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt.Select(p => Convert.ToByte(p)).ToArray()))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
