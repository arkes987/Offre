using Microsoft.EntityFrameworkCore;
using Offre.Abstraction.Interfaces.Logic;
using Offre.Abstraction.Models;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Data.Mappings.User;
using Offre.Data.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Offre.Logic.UserLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IOffreContext _offreContext;
        private readonly IUserModelMapping _userModelMapping;
        public UserLogic(IOffreContext offreContext, IUserModelMapping userModelMapping)
        {
            _offreContext = offreContext;
            _userModelMapping = userModelMapping;
        }

        public async Task<UserModel[]> GetAllUsers()
        {
            IList<User> users = await _offreContext.Users.ToListAsync();

            return users.Select(_userModelMapping.ToUserModel).ToArray();
        }

        public async Task<UserModel> GetById(long id)
        {
            var user = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return _userModelMapping.ToUserModel(user);
        }

        public async Task<UserModel> SoftDeleteUser(long id)
        {
            var user = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                user.Status = (int)UserStatusEnum.DELETED;
                user.ModifyDate = DateTime.Now;

                _offreContext.Users.Update(user);
                _offreContext.SaveChanges();

                return _userModelMapping.ToUserModel(user);
            }

            return null;
        }

        public async Task<UserModel> UpdateUser(UserModel userModel)
        {
            var existingUser = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == userModel.Id);

            if (existingUser == null)
            {
                return null;
            }

            _offreContext.Users.Update(_userModelMapping.ToUser(userModel));

            _offreContext.SaveChanges();

            return userModel;
        }

        public UserModel AddUser(UserModel userModel)
        {
            var user = _userModelMapping.ToUser(userModel);

            _offreContext.Users.Add(user);

            _offreContext.SaveChanges();

            return _userModelMapping.ToUserModel(user);
        }
    }
}
