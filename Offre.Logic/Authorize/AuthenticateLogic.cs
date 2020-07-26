using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Offre.Abstraction.Dto.Authenticate;
using Offre.Data;
using Offre.Logic.Interfaces.Authenticate;
using Offre.Validation.AuthorizePrefilters;


namespace Offre.Logic.Authenticate
{
    public class AuthenticateLogic : IAuthenticateLogic
    {
        private readonly IConfiguration _configuration;
        private readonly IOffreContext _offreContext;
        public AuthenticateLogic(IConfiguration configuration, IOffreContext offreContext)
        {
            _configuration = configuration;
            _offreContext = offreContext;
        }

        public async Task<AuthenticateResponseDto> TryAuthUser(string login, string password)
        {
            var loginPrefilterResult = new PrefilterLogin(login).MatchPrefilter();
            var passwordPrefilterResult = new PrefilterPassword(password).MatchPrefilter();

            if (loginPrefilterResult && passwordPrefilterResult)
            {

                var user = await _offreContext.Users.SingleOrDefaultAsync(x => x.Email.Equals(login) && x.Password.Equals(password));

                if (user != null)
                {
                    var token = GenerateToken(user.Id);

                    return new AuthenticateResponseDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Token = token
                    };
                }
            }

            return null;
        }
        public string GenerateToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Secret"));
            var tokenExpiryTime = _configuration.GetValue<int>("AppSettings:TokenExpiryTime");

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
