using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetAllUsers_GetAllUsersFromTable_ReturnsValidUserCount()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var users = userLogic.GetAllUsers().Result;

            Assert.IsTrue(users.Length == 2);
        }

        [TestMethod]
        public void GetById_GetsUserById_ReturnsValidModel()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var user = userLogic.GetById(13).Result;

            Assert.IsTrue(user.Id == 13);
        }

        [TestMethod]
        public void SoftDeleteUser_DeletesUserFromSet()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToDelete = userList.First();

            var updateResult = userLogic.SoftDeleteUser(modelToDelete.Id).Result;

            Assert.IsNotNull(updateResult);
            Assert.IsTrue(updateResult.Status == (int)UserStatusEnum.DELETED);
        }

        [TestMethod]
        public void SoftDeleteUser_CallsUpdateAndSavesContext()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToDelete = userList.First();

            userLogic.SoftDeleteUser(modelToDelete.Id).Wait();

            usersMock.Verify(mock => mock.Update(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateUser_CallsUpdateAndSavesContext()
        {

            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            userLogic.UpdateUser(modelToUpdate).Wait();

            usersMock.Verify(mock => mock.Update(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void UpdateUser_UpdatesUser_ReturnsValidModel()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 12,
                    Status = (int)UserStatusEnum.ACTIVE
                },
                new UserModel
                {
                    Id = 13
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var modelToUpdate = userList.First();

            modelToUpdate.Email = "test@offre.pl";

            var updateResult = userLogic.UpdateUser(modelToUpdate).Result;

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
                Id = 14,
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