using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace DataLayer.Services
{
    public interface ITokenService : IBaseService
    {
        public string GenerateToken(IEnumerable<Claim> claims);
        public string GenerateRefreshToken();
        public Dictionary<string, object> GetClaims(ClaimsIdentity identity);
        public ClaimsPrincipal GetClaims(string token);
    }


    public class TokenService : BaseService, ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration, ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {
            this._configuration = configuration;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha512);


            // VALIDATE IF AUD CLAIM EXIST TO AVOID DUPLICATION
            var exists = claims.Any(c => c.Type.Equals("aud"));

            // CREATE SECURITY TOKEN   
            JwtSecurityToken token = new(
                _configuration["Jwt:Issuer"],
                audience: (exists) ? null :_configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials
            );

            // RETURN JWT STRING FORMAT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public Dictionary<string, object> GetClaims(ClaimsIdentity identity)
        {
            Dictionary<string, object> valuePairs = new Dictionary<string, object>();

            if (identity != null)
                foreach (Claim claim in identity.Claims)
                    valuePairs.Add(claim.Type, claim.Value);

            return valuePairs;
        }

        public ClaimsPrincipal GetClaims(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false // here we are saying that we don't care about the token's expiration date
            };
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
