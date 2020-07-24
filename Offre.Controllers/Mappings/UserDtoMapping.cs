using Offre.Abstraction.Models;
using Offre.Controllers.Dto.User;

namespace Offre.Controllers.Mappings
{
    public interface IUserDtoMapping
    {
        UserResponseDto ToUserResponseDto(UserModel user);
        UserModel ToUserModel(UserDto user);
    }
    public class UserDtoMapping : IUserDtoMapping
    {
        public UserResponseDto ToUserResponseDto(UserModel user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Status = user.Status
            };
        }

        public UserModel ToUserModel(UserDto user)
        {
            return new UserModel
            {
                Id = user.Id
            };
        }
    }
}
