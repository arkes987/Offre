using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offre.Controllers.Dto.User;
using Offre.Data.Models.User;
using Offre.Logic.Interfaces.UserLogic;

namespace Offre.Controllers.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("user")]
    public class User : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public User(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<UserResponseDto> GetUserById(long id)
        {
            var user = await _userLogic.GetById(id);

            if (user == null)
            {
                return null;
            }

            return ToUserResponseDto(user);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPost]
        public async void Post()
        {

        }

        private UserResponseDto ToUserResponseDto(UserModel user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email
            };
        }
    }
}
