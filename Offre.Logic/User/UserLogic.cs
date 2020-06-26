using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Offre.Data;
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

        public async Task<UserModel> GetById(long id)
        {
            return await _offreContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
