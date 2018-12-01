using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using UniDash.Model.Models;
using Microsoft.AspNetCore.Identity;
using UniDash.BLL.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace UniDash.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _dataBase { get;}
        private IUserDetailsRepository _userDetailsRepository { get;}
        private IPermissionRepository _permissionRepository { get;}
        private UserManager<DutUser> _userManager { get;}
        private RoleManager<DutRole> _roleManager { get;}
        private SignInManager<DutUser> _signInManager { get; }

        public UserService(IUnitOfWork dataBase, IUserDetailsRepository userDetailsRepository,
            IPermissionRepository permissionRepository, UserManager<DutUser> userManager, RoleManager<DutRole> roleManager,
            SignInManager<DutUser> signInManager)
        {
            _dataBase = dataBase;
            _userDetailsRepository = userDetailsRepository;
            _permissionRepository = permissionRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
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

        public async Task<OperationDetails> Authenticate(LoginDTO userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.UserName);

            if (user == null)
                return new OperationDetails(false, "Логін або пароль не вірні");

            var result = await _signInManager.CheckPasswordSignInAsync(user, userDto.Password, false);
            if(!result.Succeeded)
                return new OperationDetails(false, "Логін або пароль не вірні");

            // TODO: допилить роли для jwt
            var roles = await _userManager.GetRolesAsync(user);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secterKey123secterKey123");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new OperationDetails(true, "", new { user = new {UserName = user.UserName}, token = tokenString});
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
    }
}
