using Offre.Data;
using Offre.Data.Models.Authorize;
using Offre.Logic.Interfaces.Authorize;
using Offre.Services.Interfaces.Authorize;
using Offre.Validation.AuthorizePrefilters;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Offre.Services.Services.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeLogic _authorizeLogic;
        private readonly IOffreContext _offreContext;
        public AuthorizeService(IAuthorizeLogic authorizeLogic, IOffreContext offreContext)
        {
            _authorizeLogic = authorizeLogic;
            _offreContext = offreContext;
        }

        public async Task<AuthorizeModel> TryAuthorizeUser(string login, string password)
        {
            var loginPrefilterResult = new PrefilterLogin(login).MatchPrefilter();
            var passwordPrefilterResult = new PrefilterPassword(password).MatchPrefilter();

            if (loginPrefilterResult && passwordPrefilterResult)
            {

                //query db for user

                var user = await _offreContext.Users.SingleOrDefaultAsync(x => x.Email.Equals(login) && x.Password.Equals(password));

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

            return null;
        }
    }
}
