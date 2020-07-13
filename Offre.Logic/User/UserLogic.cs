using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Data.Models.User;
using Offre.Logic.Interfaces.UserLogic;

namespace Offre.Logic.UserLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IOffreContext _offreContext;
        public UserLogic(IOffreContext offreContext)
        {
            _offreContext = offreContext;
        }

        public async Task<UserModel[]> GetAllUsers()
        {
            return await _offreContext.Users.ToArrayAsync();
        }

        public async Task<UserModel> GetById(long id)
        {
            var user = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
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

                return user;
            }

            return null;
        }

        public async Task<UserModel> UpdateUser(UserModel user)
        {
            var existingUser = await _offreContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            if (existingUser == null)
            {
                return null;
            }

            _offreContext.Users.Update(user);

            _offreContext.SaveChanges();

            return user;
        }

        public UserModel AddUser(UserModel user)
        {
            _offreContext.Users.Add(user);

            _offreContext.SaveChanges();

            return user;
        }
    }
}
