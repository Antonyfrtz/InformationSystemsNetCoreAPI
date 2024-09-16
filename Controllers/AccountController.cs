using InformationSystems.Server.DTO.Account;
using InformationSystems.Server.Interfaces;
using InformationSystems.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InformationSystems.Server.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
    }
}
