using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offre.Controllers.Dto.User;
using Offre.Logic.Interfaces.UserLogic;
using System.Linq;
using Offre.Controllers.Mappings;

namespace Offre.Controllers.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class User : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly IUserMapping _userMapping;
        public User(IUserLogic userLogic, IUserMapping userMapping)
        {
            _userLogic = userLogic;
            _userMapping = userMapping;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(long id)
        {
            var user = await _userLogic.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(_userMapping.ToUserResponseDto(user));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetAllUsers()
        {
            var users = await _userLogic.GetAllUsers();

            return Ok(users?.Select(_userMapping.ToUserResponseDto).ToArray());
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPost]
        public ActionResult<UserResponseDto> AddUser(UserDto user)
        {
            var userAdded = _userLogic.AddUser(_userMapping.ToUserModel(user));

            return Ok(userAdded);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(UserDto user)
        {
            var updatedUser = await _userLogic.UpdateUser(_userMapping.ToUserModel(user));

            if (updatedUser == null)
                return NotFound();

            return Ok(_userMapping.ToUserResponseDto(updatedUser));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserResponseDto>> DeleteUser(long id)
        {
            var user = await _userLogic.SoftDeleteUser(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
