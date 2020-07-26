using Offre.Abstraction.Dto.User;
using Offre.Data.Models.User;

namespace Offre.Abstraction.Mappings.User
{
    public interface IUserMapping
    {
        UserResponseDto ToUserResponseDto(UserModel user);
        UserModel ToUserModel(UserDto user);
    }
    public class UserMapping : IUserMapping
    {
        public UserModel ToUserModel(UserDto user) => new UserModel
        {
            Id = user.Id,
            Email = user.Email
        };

        public UserResponseDto ToUserResponseDto(UserModel user) => new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            Status = user.Status
        };
    }
}
