using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Data.Models.User;
using Offre.Logic.UserLogic;

namespace Offre.Test.User
{
    [TestClass]
    public class UserLogicTests
    {
        private readonly Mock<IOffreContext> _offreContextMock = new Mock<IOffreContext>();

        private UserLogic GetTestSubject()
        {
            return new UserLogic(_offreContextMock.Object);
        }

        [TestMethod]
        public async Task GetAllUsers_GetAllUsersFromTable_ReturnsValidUserCount()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0
                },
                new UserModel
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

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0
                },
                new UserModel
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

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
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

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToDelete = userList.First();

            await userLogic.SoftDeleteUser(modelToDelete.Id);

            usersMock.Verify(mock => mock.Update(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_CallsUpdateAndSavesContext()
        {

            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            await userLogic.UpdateUser(modelToUpdate);

            usersMock.Verify(mock => mock.Update(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_UpdatesUser_ReturnsValidModel()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 1
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            modelToUpdate.Email = "test@offre.pl";

            var updateResult = await userLogic.UpdateUser(modelToUpdate);

            Assert.IsNotNull(updateResult);
            Assert.IsTrue(updateResult.Email.Equals("test@offre.pl"));
        }

        [TestMethod]
        public void AddUser_AddsUserAndSaveContext()
        {
            Mock<DbSet<UserModel>> userSetMock = new Mock<DbSet<UserModel>>();
            _offreContextMock.Setup(mock => mock.Users).Returns(userSetMock.Object);

            var userLogic = GetTestSubject();

            var userModel = new UserModel
            {
                Id = 0,
                Email = "test@offre.pl",
                Status = (int)UserStatusEnum.ACTIVE,
                Password = "QWERTYU",
                SaveDate = DateTime.Now,
                ModifyDate = DateTime.Now
            };

            userLogic.AddUser(userModel);

            userSetMock.Verify(mock => mock.Add(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }
    }
}