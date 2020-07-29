using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MockQueryable.Moq;
using Moq;
using Offre.Abstraction.Dto.User;
using Offre.Abstraction.Mappings.User;
using Offre.Data;
using Offre.Data.Enums;
using Offre.Data.Models.User;
using Offre.Logic.UserLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Offre.Test.User
{
    [TestClass]
    public class UserLogicTests
    {
        private readonly Mock<IOffreContext> _offreContextMock = new Mock<IOffreContext>();
        private readonly Mock<IUserMapping> _userMappingMock = new Mock<IUserMapping>();

        private UserLogic GetTestSubject()
        {
            var mapperInstance = new UserMapping();
            _userMappingMock.Setup(mock => mock.ToUserResponseDto(It.IsAny<UserModel>())).Returns((UserModel userModel) => mapperInstance.ToUserResponseDto(userModel));
            _userMappingMock.Setup(mock => mock.ToUserModel(It.IsAny<UserDto>())).Returns((UserDto userDto) => mapperInstance.ToUserModel(userDto));

            return new UserLogic(_offreContextMock.Object, _userMappingMock.Object);
        }

        [TestMethod]
        public async Task GetAllUsers_GetsAllUsersFromTable_ReturnsValidUserCount()
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
                },
                new UserModel
                {
                    Id = 3
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);

            var users = await userLogic.GetAllUsers();

            Assert.AreEqual(3, users.Length);
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

            Assert.AreEqual(0, user.Id);
        }

        [TestMethod]
        public async Task SoftDeleteUser_SoftDeleteUserInDatabase_ProperlyReturnsUpdatedRecordWithDeletedStatus()
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
            Assert.AreEqual((int)UserStatusEnum.DELETED, updateResult.Status);
        }

        [TestMethod]
        public async Task SoftDeleteUser_SoftDeleteUserInDatabase_CallsSaveChanges()
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

            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_UpdateUserInDatabase_CallsSaveChanges()
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


            await userLogic.UpdateUser(new UserDto
            {
                Id = 0
            });

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

            var updateResult = await userLogic.UpdateUser(new UserDto
            {
                Id = 0,
                Email = "test@offre.pl"
            });

            Assert.IsNotNull(updateResult);
            Assert.AreEqual("test@offre.pl", updateResult.Email);
        }

        [TestMethod]
        public async Task UpdateUser_UpdatesUser_ProperlyPropagateChanges()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE,
                    Email = "test@offre.pl",
                    Password = "ABCD!@#",
                    ModifyDate = DateTime.Now,
                    SaveDate = DateTime.Now
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);


            var updateResult = await userLogic.UpdateUser(new UserDto
            {
                Id = 0,
                Email = "support@offre.pl"
            });


            Assert.AreEqual("support@offre.pl", updateResult.Email);
            
        }

        [TestMethod]
        public async Task UpdateUser_UpdatesUser_NotPropagateChangesWithIgnoreAttribute()
        {
            var userLogic = GetTestSubject();

            var userList = new List<UserModel>
            {
                new UserModel
                {
                    Id = 0,
                    Status = (int)UserStatusEnum.ACTIVE,
                    Email = "test@offre.pl",
                    Password = "ABCD!@#",
                    ModifyDate = DateTime.Now,
                    SaveDate = DateTime.Now
                }
            };

            var usersMock = userList.AsQueryable().BuildMockDbSet();

            _offreContextMock.Setup(mock => mock.Users).Returns(usersMock.Object);


            var updateResult = await userLogic.UpdateUser(new UserDto
            {
                Id = 0,
                Status = (int)UserStatusEnum.DELETED
            });


            Assert.AreEqual((int)UserStatusEnum.ACTIVE, updateResult.Status);

        }

        [TestMethod]
        public void AddUser_AddsNewUserToDatabase_ProperlyReturnsNewUser()
        {
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

            _offreContextMock.Setup(mock => mock.Users.Add(It.IsAny<UserModel>())).Callback((UserModel userModel) =>
            {
                userList.Add(userModel);
            });

            var userLogic = GetTestSubject();

            var userResponseDto = userLogic.AddUser(new UserDto
            {
                Id = 2,
                Email = "test@offre.pl"
            });

            Assert.AreEqual(2, userResponseDto.Id);
            Assert.AreEqual("test@offre.pl", userResponseDto.Email);
        }

        [TestMethod]
        public void AddUser_AddsNewUserToDatabase_CallsSaveChanges()
        {
            Mock<DbSet<UserModel>> userSetMock = new Mock<DbSet<UserModel>>();
            _offreContextMock.Setup(mock => mock.Users).Returns(userSetMock.Object);

            var userLogic = GetTestSubject();

            userLogic.AddUser(new UserDto
            {
                Id = 0
            });

            userSetMock.Verify(mock => mock.Add(It.IsAny<UserModel>()), Times.Once);
            _offreContextMock.Verify(mock => mock.SaveChanges(), Times.Once);
        }
    }
}