using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Data.Mappings.User;
using Offre.Logic.UserLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Offre.Abstraction.Models;

namespace Offre.Test.User
{
    [TestClass]
    public class UserLogicTests
    {
        private readonly Mock<IOffreContext> _offreContextMock = new Mock<IOffreContext>();
        private readonly Mock<IUserModelMapping> _userModelMappingMock = new Mock<IUserModelMapping>();

        private UserLogic GetTestSubject()
        {
            return new UserLogic(_offreContextMock.Object, _userModelMappingMock.Object);
        }

        private void SetUserModelMapping()
        {
            _userModelMappingMock.Setup(mock => mock.ToUser(It.IsAny<UserModel>())).Returns(new Data.Models.User.User
            {

            });

            _userModelMappingMock.Setup(mock => mock.ToUserModel(It.IsAny<Data.Models.User.User>())).Returns(new UserModel
            {

            });
        }

        [TestMethod]
        public async Task GetAllUsers_GetAllUsersFromTable_ReturnsValidUserCount()
        {
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var users = await userLogic.GetAllUsers();

            Assert.IsTrue(users.Length == 2);
        }

        [TestMethod]
        public async Task GetById_GetsUserById_ReturnsValidModel()
        {
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var user = await userLogic.GetById(0);

            Assert.IsTrue(user.Id == 0);
        }

        [TestMethod]
        public async Task SoftDeleteUser_DeletesUserFromSet()
        {
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToDelete = userList.First();

            var updateResult = await userLogic.SoftDeleteUser(modelToDelete.Id);

            Assert.IsNotNull(updateResult);
            Assert.IsTrue(updateResult.Status == (int)UserStatusEnum.DELETED);
        }

        [TestMethod]
        public async Task SoftDeleteUser_CallsUpdateAndSavesContext()
        {
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToDelete = userList.First();

            await userLogic.SoftDeleteUser(modelToDelete.Id);

            usersMock.Verify(mock => mock.Update(It.IsAny<Data.Models.User.User>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_CallsUpdateAndSavesContext()
        {
            SetUserModelMapping();
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            await userLogic.UpdateUser(_userModelMappingMock.Object.ToUserModel(modelToUpdate));

            usersMock.Verify(mock => mock.Update(It.IsAny<Data.Models.User.User>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_UpdatesUser_ReturnsValidModel()
        {
            SetUserModelMapping();
            var userLogic = GetTestSubject();

            var userList = new List<Data.Models.User.User>
            {
                new Data.Models.User.User
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new Data.Models.User.User
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            modelToUpdate.Email = "test@offre.pl";

            var updateResult = await userLogic.UpdateUser(_userModelMappingMock.Object.ToUserModel(modelToUpdate));

            Assert.IsNotNull(updateResult);
            Assert.IsTrue(updateResult.Email.Equals("test@offre.pl"));
        }

        [TestMethod]
        public void AddUser_AddsUserAndSaveContext()
        {
            SetUserModelMapping();
            Mock<DbSet<Data.Models.User.User>> userSetMock = new Mock<DbSet<Data.Models.User.User>>();
            _offreContextMock.Setup(mock => mock.Users).Returns(userSetMock.Object);

            var userLogic = GetTestSubject();

            var userModel = new Data.Models.User.User
            {
                Id = 0,
                Email = "test@offre.pl",
                Status = (int)UserStatusEnum.ACTIVE,
                Password = "QWERTYU",
                SaveDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            userLogic.AddUser(_userModelMappingMock.Object.ToUserModel(userModel));

            userSetMock.Verify(mock => mock.Add(It.IsAny<Data.Models.User.User>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }
    }
}