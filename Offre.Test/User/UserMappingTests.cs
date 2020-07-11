using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Offre.Controllers.Dto.User;
using Offre.Controllers.Mappings;
using Offre.Data.Enums;
using Offre.Data.Models.User;

namespace Offre.Test.User
{
    [TestClass]
    public class UserMappingTests
    {
        private UserMapping GetTestSubject()
        {
            return new UserMapping();
        }

        [TestMethod]
        public void ToUserResponseDto_ChecksResult_ReturnsValidResponse()
        {
            var userMapping = GetTestSubject();

            var userModel = new UserModel
            {
                Id = 14,
                Email = "test@offre.pl",
                Status = (int)UserStatusEnum.ACTIVE,
                Password = "QWERTYU",
                SaveDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            var result = userMapping.ToUserResponseDto(userModel);

            Assert.AreEqual(userModel.Id, result.Id);
            Assert.AreEqual(userModel.Email, result.Email);
            Assert.AreEqual(userModel.Status, result.Status);
        }

        [TestMethod]
        public void ToUserModel_ChecksResult_ReturnsValidModel()
        {
            var userMapping = GetTestSubject();

            var userDto = new UserDto
            {
                Id = 14,
            };

            var result = userMapping.ToUserModel(userDto);

            Assert.AreEqual(userDto.Id, result.Id);

        }
    }
}
