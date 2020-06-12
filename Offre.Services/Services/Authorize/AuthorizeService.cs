using Offre.Data.Models.Authorize;
using Offre.Logic.Interfaces.Authorize;
using Offre.Services.Interfaces.Authorize;
using Offre.Validation.AuthorizePrefilters;

namespace Offre.Services.Services.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IAuthorizeLogic _authorizeLogic;
        public AuthorizeService(IAuthorizeLogic authorizeLogic)
        {
            _authorizeLogic = authorizeLogic;
        }

        public AuthorizeModel TryAuthorizeUser(string login, string password)
        {
            var loginPrefilterResult = new PrefilterLogin(login).MatchPrefilter();
            var passwordPrefilterResult = new PrefilterPassword(password).MatchPrefilter();

            if (loginPrefilterResult && passwordPrefilterResult)
            {

                //query db for user

                //generate JWT token
                var token = _authorizeLogic.GenerateToken(0);

                return new AuthorizeModel
                {
                    Secret = token
                };
            }


            return new AuthorizeModel();
        }
    }
}
