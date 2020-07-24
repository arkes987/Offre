using Offre.Data.Models.Authorize;
using System.Threading.Tasks;

namespace Offre.Logic.Interfaces.Authorize
{
    public interface IAuthorizeLogic
    {
        string GenerateToken(long userId);
        Task<AuthorizeModel> TryAuthorizeUser(string login, string password);
    }
}
