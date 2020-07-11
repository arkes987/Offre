using System;
using System.Linq;
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
            var user = await _offreContext.Users.FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }

            throw new Exception();
        }

        public void SoftDeleteUser(long id)
        {
            var user = _offreContext.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.Status = (int)UserStatusEnum.DELETED;
                user.ModifyDate = DateTime.Now;
                _offreContext.SaveChanges();
            }
            else
            {
                throw new Exception();
            }
        }

        public UserModel UpdateUser(UserModel user)
        {
            //here we need to check if user exsists

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
