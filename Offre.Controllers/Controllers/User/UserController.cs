using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Offre.Logic.Interfaces.UserLogic;
using Offre.Abstraction.Dto.User;

namespace Offre.Controllers.Controllers.User
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(long id)
        {
            var user = await _userLogic.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet]
        public async Task<ActionResult<UserResponseDto>> GetAllUsers()
        {
            var users = await _userLogic.GetAllUsers();

            return Ok(users);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPost]
        public ActionResult<UserResponseDto> AddUser(UserDto user)
        {
            var userAdded = _userLogic.AddUser(user);

            return Ok(userAdded);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(UserDto user)
        {
            var updatedUser = await _userLogic.UpdateUser(user);

            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
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
