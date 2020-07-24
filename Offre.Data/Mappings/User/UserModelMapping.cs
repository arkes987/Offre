using Offre.Abstraction.Models;

namespace Offre.Data.Mappings.User
{
    public interface IUserModelMapping
    {
        UserModel ToUserModel(Models.User.User user);
        Models.User.User ToUser(UserModel userModel);
    }
    public class UserModelMapping : IUserModelMapping
    {
        public UserModel ToUserModel(Models.User.User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Status = user.Status,
                Email = user.Email,
                SaveDate = user.SaveDate,
                ModifyDate = user.ModifyDate,
                Password = user.Password
            };
        }

        public Models.User.User ToUser(UserModel userModel)
        {
            return new Models.User.User
            {
                Id = userModel.Id,
                Status = userModel.Status,
                SaveDate = userModel.SaveDate,
                Email = userModel.Email,
                Password = userModel.Password,
                ModifyDate = userModel.ModifyDate
            };
        }
    }
}
