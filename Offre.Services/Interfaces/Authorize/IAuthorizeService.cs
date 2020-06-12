using Offre.Data.Models.Authorize;

namespace Offre.Services.Interfaces.Authorize
{
    public interface IAuthorizeService
    {
        AuthorizeModel TryAuthorizeUser(string login, string password);
    }
}
