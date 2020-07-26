using System.Threading.Tasks;
using Offre.Abstraction.Dto.Authenticate;

namespace Offre.Logic.Interfaces.Authenticate
{
    public interface IAuthenticateLogic
    {
        string GenerateToken(long userId);
        Task<AuthenticateResponseDto> TryAuthUser(string login, string password);
    }
}
