using System.Threading.Tasks;
using Offre.Data.Models.Authorize;

namespace Offre.Logic.Interfaces.Authorize
{
    public interface IAuthorizeLogic
    {
        string GenerateToken(long userId);
        Task<AuthorizeModel> TryAuthorizeUser(string login, string password);
    }
}
