using System;
using System.Collections.Generic;
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
            return await _offreContext.Users.FirstOrDefaultAsync(user => user.Id == id);
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

        public async Task<UserModel> UpdateUser(UserModel user)
        {
           // var user = await _offreContext.Users.Update(user);

            throw new NotImplementedException();
        }

        public async Task<UserModel> AddUser(UserModel user)
        {
            //var addedUser = await _offreContext.Users.AddAsync(user);
            
            //_offreContext.SaveChanges();

            //return addedUser;

            throw new NotImplementedException();
        }
    }
}
