using System;
using Offre.Controllers.Dto.User;
using Offre.Data.Models.User;

namespace Offre.Controllers.Mappings
{
    public interface IUserMapping
    {
        UserResponseDto ToUserResponseDto(UserModel user);
        UserModel ToUserModel(UserDto user);
    }
    public class UserMapping : IUserMapping
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
