using eCommerce.Core.entities;
using eCommerce.Core.entities.identity;
using eCommerce.DTO;
using eCommerce.Erros;
using eCommerce.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        // legacy way
        //[HttpPost("Login")]
        //public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        //{
        //    var user = await userManager.FindByEmailAsync(loginDTO.Email);
        //    if (user == null)
        //    {
        //        return Unauthorized(new ApiResponse(401));
        //    }

        //    var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        //    if (!result.Succeeded)
        //    {
        //        return Unauthorized(new ApiResponse(401));
        //    }

        //    return new UserDTO
        //    {
        //        Email = user.Email,
        //        Token = "",
        //        DisplayName = user.DisplayName
        //    };
        //}

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                
                Email = registerDto.Email,
                UserName = registerDto.Email,
                address = new Address()
            };

            var result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        
        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if(User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }

            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);

            return Ok(new
            {
                user.UserName,
                user.Email,
                Address = user.address?.toDTO(),
                Roles = User.FindFirstValue(ClaimTypes.Role)
            });
        }

        [HttpGet]
        public ActionResult GetAuth()
        {
            return Ok(new {IsAuthenticated = User.Identity?.IsAuthenticated ?? false});
        }

        [Authorize]
        [HttpPost("address")]
        public async Task<ActionResult<Address>> CreateIrUpdateAddress(AddressDto addressDTO)
        {
            var user = await signInManager.UserManager.GetUserByEmailWithAddress(User);
            if (user == null)
            {
                user.address = addressDTO.toEntitty();
            }
            else
            {
                user.address.UpdateFromDTO(addressDTO);
            }

            var result = await signInManager.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Problem while updating the user");
            }

            return Ok(user.address.toDTO());
        }

    }
}
