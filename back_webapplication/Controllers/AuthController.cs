using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webapplication.DtoModels;
using webapplication.Models;
using webapplication.Services;
using webapplication.Utils;

namespace webapplication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ITokenService tokenService;
        public static Guid userId;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            this.authService = authService;
            this.tokenService = tokenService;
        }
        // GET api/values
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            if (authService.GetUser(user.Email, user.Password) != null)
            {
                var item = authService.GetUserFromEmail(user.Email);
                userId = item.Id;
                if (item == null)
                {
                    return Unauthorized();
                }
                var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, "User")
                };
                var accessToken = tokenService.GenerateAccessToken(claims);
                var refreshToken = tokenService.GenerateRefreshToken();

                item.RefreshToken = refreshToken;
                item.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Utilities.ExpireMinutes);

                authService.UpdateUser(item);
                return Ok(new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken
                });
            }
            return Unauthorized();
        }
        [HttpPost, Route("register")]
        public IActionResult Register([FromBody]RegisterModel user) 
        {  //TODO --- AutoMapper
            User objUser = new User();
            objUser.FirstName = user.FirstName;
            objUser.lastName = user.LastName;
            objUser.Email = user.Email;
            objUser.Password = user.Password;
            bool isRegistrated = authService.Register(objUser);
            if (isRegistrated) 
            {
                return Ok();
            }

            return BadRequest("User with that email Exists!");
        }
    }
}
