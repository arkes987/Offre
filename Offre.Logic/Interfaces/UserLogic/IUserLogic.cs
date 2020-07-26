using System.Threading.Tasks;
using Offre.Abstraction.Dto.User;
using Offre.Data.Models.User;

namespace Offre.Logic.Interfaces.UserLogic
{
    public interface IUserLogic
    {
        Task<UserResponseDto[]> GetAllUsers();
        Task<UserResponseDto> GetById(long id);
        Task<UserResponseDto> SoftDeleteUser(long id);
        Task<UserResponseDto> UpdateUser(UserDto user);
        UserResponseDto AddUser(UserDto user);
    }
}
