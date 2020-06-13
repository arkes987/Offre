using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Offre.Logic.Interfaces.Authorize;
using Microsoft.Extensions.Configuration.Json;


namespace Offre.Logic.Authorize
{
    public class AuthorizeLogic : IAuthorizeLogic
    {
        private readonly IConfiguration _configuration;
        public AuthorizeLogic(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Secret"));

            var tokenExpiryTime = Convert.ToInt32(_configuration.GetValue<string>("AppSettings:TokenExpiryTime"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(tokenExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
