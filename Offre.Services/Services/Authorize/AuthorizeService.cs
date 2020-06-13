using Offre.Data;
using Offre.Data.Models.Authorize;
using Offre.Logic.Interfaces.Authorize;
using Offre.Services.Interfaces.Authorize;
using Offre.Validation.AuthorizePrefilters;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Offre.Services.Services.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeLogic _authorizeLogic;
        public AuthorizeService(IAuthorizeLogic authorizeLogic)
        {
            _authorizeLogic = authorizeLogic;
        }

        public async Task<AuthorizeModel> TryAuthorizeUser(string login, string password)
        {
            var loginPrefilterResult = new PrefilterLogin(login).MatchPrefilter();
            var passwordPrefilterResult = new PrefilterPassword(password).MatchPrefilter();

            if (loginPrefilterResult && passwordPrefilterResult)
            {

                //query db for user

                using (var db = new OffreContext())
                {
                    var user = await db.Users.SingleOrDefaultAsync(x => x.Email.Equals(login) && x.Password.Equals(password));

                    if (user != null)
                    {
                        //generate JWT token
                        var token = _authorizeLogic.GenerateToken(user.Id);

                        return new AuthorizeModel
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Token = token
                        };
                    }

                }
            }

            return null;
        }
    }
}
