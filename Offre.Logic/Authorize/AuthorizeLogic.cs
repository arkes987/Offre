using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Offre.Logic.Interfaces.Authorize;
using Microsoft.Extensions.Configuration.Json;


namespace Offre.Logic.Authorize
{
    public class AuthorizeLogic : IAuthorizeLogic
    {
        private const string Secret = "L2zENCYuVozGOR9EQJN0";
        private const int TokenExpiryTime = 7;

        public AuthorizeLogic()
        {

        }
        public string GenerateToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(TokenExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
