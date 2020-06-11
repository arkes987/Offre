using Offre.Services.Interfaces.Authorize;
using Offre.Validation.AuthorizePrefilters;

namespace Offre.Services.Services.Authorize
{
    public class AuthorizeService : IAuthorizeService
    {
        public AuthorizeService()
        {
            
        }

        public object TryAuthorizeUser(string login, string password)
        {
            var loginPrefilterResult = new PrefilterLogin(login).MatchPrefilter();
            var passwordPrefilterResult = new PrefilterPassword(password).MatchPrefilter();

            if (loginPrefilterResult && passwordPrefilterResult)
            {
                return true;
            }


            return false;
        }
    }
}
