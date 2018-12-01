using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniDash.BLL.DTO;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.Model.Models;

namespace UniDash.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private IUserService _userService { get; }
        private UserManager<DutUser> _userManager { get; }

        public AccountController(IUserService userService, UserManager<DutUser> userManager)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [Route("login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<OperationDetails> Authenticate([FromBody] LoginDTO user)
        {
            if (!ModelState.IsValid)
                return new OperationDetails(false, "Не всі поля заповнені");           

            return await _userService.Authenticate(user);
        }

        /*public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            if (!ModelState.IsValid)
                return Ok();

            return Ok();
        }*/
    }
}
