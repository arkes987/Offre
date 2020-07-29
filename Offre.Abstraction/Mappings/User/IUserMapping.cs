using Offre.Abstraction.Dto.User;
using Offre.Data.Models.User;

namespace Offre.Abstraction.Mappings.User
{
    public interface IUserMapping
    {
        UserResponseDto ToUserResponseDto(UserModel user);
        UserModel ToUserModel(UserDto user);
    }
}
