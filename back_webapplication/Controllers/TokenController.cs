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
using Microsoft.AspNetCore.Http;
using webapplication.Utils;

namespace webapplication.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IAuthService authService;
        public TokenController(ITokenService tokenService, IAuthService authService)
        {
            this.tokenService = tokenService;
            this.authService = authService;
        }
        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }
            string accessToken = tokenModel.AccessToken;
            string refreshToken = tokenModel.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            var email = principal.Identity.Name;

            var user = authService.GetUserFromEmail(email);
            if (user == null || user.RefreshToken != refreshToken)
            {
                return BadRequest("Invalid client request");
            }
            var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(Utilities.ExpireMinutes);
            authService.UpdateUser(user);

            return  new ObjectResult(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });

        }
        [HttpPost]
        [Route("revoke")]
        public IActionResult Revoke() 
        {
            var email = User.Identity.Name;

            var user = authService.GetUserFromEmail(email);
            if (user == null) { 
                return BadRequest();
            }
            user.RefreshToken = null;
            authService.UpdateUser(user);
            return NoContent();
        }
    }
}
