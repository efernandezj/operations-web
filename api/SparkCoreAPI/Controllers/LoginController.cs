using AutoMapper;
using CoreFramework.Helper;
using DataLayer.DTOs;
using DataLayer.Entities;
using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SparkCoreAPI.Controllers
{
    [ApiController]
    [Route("api/login")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public LoginController(ISecurityService securityService , ITokenService tokenService, IMapper mapper)
        {
            this._securityService = securityService;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string message = string.Empty;

            try
            {
                // Get token claims
                Dictionary<string, object> claims = new Dictionary<string, object>();
                claims = this.tokenService.GetClaims(HttpContext.User.Identity as ClaimsIdentity);

                if (claims.Count == 0)
                    throw new CustomException("Claims not found");


                // Get User
                User oUser = await _securityService.GetUser(claims["username"].ToString());

                // Validate User
                if (oUser == null || (oUser != null && !oUser.IsActive))
                    throw new CustomException("Account Not Found");

                

                return Ok(new
                {
                    IsCorrect = true,
                    Data = this.mapper.Map<AccountInfoShort>(oUser.Employee)
                });
            }
            catch (CustomException oEx) { message = oEx.Message; }
            catch (Exception oEx)
            {

                return BadRequest(oEx.Message);
            }

            return Ok(new { IsCorrect = false, message = message });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post(UserCredentials user)
        {
            string message = string.Empty;

            try
            {
                // USER EXIST
                var oUser = await _securityService.GetUser(user.Username);                

                if (oUser == null)
                    throw new CustomException($"User {user.Username} does not exist!");


                // PWD VALIDATION
                PasswordHash oPwdHash = _securityService.Hash(user.Password, user.Username);

                if (oUser.Password != oPwdHash.Hash)
                    throw new CustomException("Incorrect password!");


                // USER ACTIVE
                if (!oUser.IsActive)
                    throw new CustomException($"User {oUser.Username} is not active!");


                // CLAIMS
                 var claims = new[]
                {
                    new Claim("uid", oUser.Workday.ToString()),
                    new Claim("username", oUser.Username)
                };


                // STORE REFRESH TOKEN EXPIRATION
                var refreshToken = this.tokenService.GenerateRefreshToken();
                oUser.RefreshToken = refreshToken;
                oUser.RefreshTokenExp = ((DateTimeOffset)DateTime.Now.AddDays(5)).ToUnixTimeSeconds();
                await this.tokenService.SaveChangesAsync();

                return Ok(new
                {
                    IsCorrect = true,
                    AuthenticatedResponse = new AuthenticatedResponse { AccessToken = this.tokenService.GenerateToken(claims) , RefreshToken = refreshToken }
                });
            }
            catch (CustomException oEx) { message = oEx.Message; }
            catch (Exception oEx)
            {

                return BadRequest(oEx.Message);
            }

            return Ok(new { IsCorrect = false, message = message });
        }
    }
}
