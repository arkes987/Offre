using System.Threading.Tasks;
using Offre.Data.Models.User;

namespace Offre.Logic.Interfaces.UserLogic
{
    public interface IUserLogic
    {
        Task<UserModel> GetById(long id);
    }
}
