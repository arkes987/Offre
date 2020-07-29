using Offre.Data.Attributes;
using Offre.Data.Models.User;
using System.Linq;

namespace Offre.Logic.Mappings.User
{
    public static class UserPopulate
    {
        public static void ShallowPopulateChanges(this UserModel userModel, UserModel changes)
        {
            foreach (var property in userModel.GetType().GetProperties())
            {
                if (property.GetCustomAttributes(false).Any(attribute => attribute.GetType().Name.Equals(nameof(IgnorePopulate))))
                {
                    continue;
                }

                var changedModelProperties = changes.GetType().GetProperties();

                var changedProperty = changedModelProperties.FirstOrDefault(x => x.Name.Equals(property.Name) && x.PropertyType == property.PropertyType);

                if (changedProperty == null) continue;

                var changedPropertyValue = changedProperty.GetValue(changes);

                property.SetValue(userModel, changedPropertyValue);
            }
        }
    }
}
