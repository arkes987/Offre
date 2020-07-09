using System.Threading.Tasks;
using Offre.Data.Models.User;

namespace Offre.Logic.Interfaces.UserLogic
{
    public interface IUserLogic
    {
        Task<UserModel[]> GetAllUsers();
        Task<UserModel> GetById(long id);
        void SoftDeleteUser(long id);
        Task<UserModel> UpdateUser(UserModel user);
        Task<UserModel> AddUser(UserModel user);
    }
}
