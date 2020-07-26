using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Offre.Abstraction.Dto.User;
using Offre.Abstraction.Mappings.User;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Logic.Interfaces.UserLogic;

namespace Offre.Logic.UserLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IOffreContext _offreContext;
        private readonly IUserMapping _userMapping;
        public UserLogic(IOffreContext offreContext, IUserMapping userMapping)
        {
            _offreContext = offreContext;
            _userMapping = userMapping;
        }

        public async Task<UserResponseDto[]> GetAllUsers()
        {
            var users = await _offreContext.Users.ToArrayAsync();

            return users.Select(_userMapping.ToUserResponseDto).ToArray();
        }

        public async Task<UserResponseDto> GetById(long id)
        {
            var user = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
                return _userMapping.ToUserResponseDto(user);

            return null;
        }

        public async Task<UserResponseDto> SoftDeleteUser(long id)
        {
            var user = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                user.Status = (int)UserStatusEnum.DELETED;
                user.ModifyDate = DateTime.Now;
                _offreContext.Users.Update(user);
                _offreContext.SaveChanges();

                return _userMapping.ToUserResponseDto(user);
            }

            return null;
        }

        public async Task<UserResponseDto> UpdateUser(UserDto user)
        {
            var existingUser = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser == null)
            {
                return null;
            }

            //here need to populate changes from user dto to existing user on db
            existingUser.Email = user.Email;
            _offreContext.Users.Update(existingUser);

            _offreContext.SaveChanges();

            return _userMapping.ToUserResponseDto(existingUser);
        }

        public UserResponseDto AddUser(UserDto user)
        {
            var userModel = _userMapping.ToUserModel(user);

            _offreContext.Users.Add(userModel);

            _offreContext.SaveChanges();

            return _userMapping.ToUserResponseDto(userModel);
        }
    }
}
