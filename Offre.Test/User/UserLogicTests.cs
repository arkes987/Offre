using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        private readonly Mock<DbSet<UserModel>> _userSetMock = new Mock<DbSet<UserModel>>();
        private readonly Mock<IOffreContext> _offreContextMock = new Mock<IOffreContext>();

        private UserLogic GetTestSubject()
        {
            return new UserLogic(_offreContextMock.Object);
        }

        //[TestMethod]
        //public void GetAllUsers()
        //{
        //    var userLogic = GetTestSubject();


        //    _userSetMock.Object.Add(new UserModel
        //    {
        //        Id = 12
        //    });

        //    _userSetMock.Object.Add(new UserModel
        //    {
        //        Id = 14
        //    });

        //    _offreContextMock.Setup(mock => mock.Users).Returns(_userSetMock.Object);

        //    var users = userLogic.GetAllUsers().Result;

        //    Assert.IsTrue(users.Length == 2);
        //}

        //[TestMethod]
        //public void GetById()
        //{
        //    var userLogic = GetTestSubject();
        //}

        //[TestMethod]
        //public void SoftDeleteUser()
        //{
        //    var userLogic = GetTestSubject();
        //}

        [TestMethod]
        public void UpdateUser()
        {
            _offreContextMock.Setup(mock => mock.Users).Returns(_userSetMock.Object);

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

            userLogic.UpdateUser(userModel);

            _userSetMock.Verify(mock => mock.Update(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void AddUser_SaveUser()
        {
            _offreContextMock.Setup(mock => mock.Users).Returns(_userSetMock.Object);
            
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

            _userSetMock.Verify(mock => mock.Add(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);

        }
    }
}
