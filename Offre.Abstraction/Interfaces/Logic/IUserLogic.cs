using Offre.Abstraction.Models;
using System.Threading.Tasks;

namespace Offre.Abstraction.Interfaces.Logic
{
    public interface IUserLogic
    {
        Task<UserModel[]> GetAllUsers();
        Task<UserModel> GetById(long id);
        Task<UserModel> SoftDeleteUser(long id);
        Task<UserModel> UpdateUser(UserModel user);
        UserModel AddUser(UserModel user);
    }
}
