using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QTicket.api.Dtos;
using QTicket.api.Entities;
using QTicket.api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTicket.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppUserService _appUserService;
        private readonly TokenService _tokenService;

        public AccountController(AppUserService appUserService, TokenService tokenService)
        {
            _appUserService = appUserService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<AppUser>>> Get()
        {
            return await _appUserService.Get();
        }


        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("User already exists");

            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                Password = registerDto.Password
            };

            user = await _appUserService.Create(user);
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var appUser = await _appUserService.Login(loginDto);

            if (appUser == null) return BadRequest("Invalid username or password");

            return new UserDto
            { 
                Username = appUser.UserName,
                Token = _tokenService.CreateToken(appUser)
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _appUserService.UserExists(username.ToLower());
        }
    }
}
