using System.Threading.Tasks;
using Offre.Data.Models.Authorize;

namespace Offre.Services.Interfaces.Authorize
{
    public interface IAuthorizeService
    {
        Task<AuthorizeModel> TryAuthorizeUser(string login, string password);
    }
}
