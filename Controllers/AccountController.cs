using InformationSystems.Server.DTO.Account;
using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InformationSystems.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var user = new AppUser 
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                var createdUser = await _userManager.CreateAsync(user, model.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDTO
                            {
                                Username = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                return BadRequest(createdUser.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == model.Username.ToLower());
                if (user == null) return Unauthorized("Invalid username!");
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    return Ok(
                        new NewUserDTO
                        {
                            Username = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        }
                    );
                }
                return Unauthorized("Username not found and/or password incorrect!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
