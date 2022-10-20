using CoreFramework.Helper;
using DataLayer.DTOs;
using DataLayer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SparkCoreAPI.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly ISecurityService _securityService;

        public TokenController(ITokenService tokenService, ISecurityService securityService)
        {
            this.tokenService = tokenService;
            this._securityService = securityService;
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(AuthenticatedResponse token)
        {
            string message = "";
            try
            {
                if (token == null)
                    return BadRequest("Invalid client request");

                // GET TOKEN CLAIMS
                ClaimsPrincipal claims = this.tokenService.GetClaims(token.AccessToken);
                string username = claims.Claims.FirstOrDefault(x => x.Type.Equals("username", StringComparison.OrdinalIgnoreCase)).Value;

                // GET USER
                var oUser = await _securityService.GetUser(username);


                // VALIDATE REFRESH TOKEN
                long RefreshTokenExp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
                if (oUser == null || oUser.RefreshToken != token.RefreshToken || oUser.RefreshTokenExp <= RefreshTokenExp)
                    return BadRequest();

                // GENERATE AND SAVE NEW REFRESH TOKEN
                var newRefreshToken = this.tokenService.GenerateRefreshToken();
                oUser.RefreshToken = newRefreshToken;
                await this.tokenService.SaveChangesAsync();


                return Ok(new AuthenticatedResponse { AccessToken = this.tokenService.GenerateToken(claims.Claims.Distinct()), RefreshToken = newRefreshToken });
  
            }
            catch (CustomException e) { message = e.Message; }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            return Ok(new { IsCorrect = false, message = message });

        }



        //[HttpPost("revoke")]
        //public async Task<IActionResult> Revoke()
        //{
        //    return Ok();
        //}
    }
}
