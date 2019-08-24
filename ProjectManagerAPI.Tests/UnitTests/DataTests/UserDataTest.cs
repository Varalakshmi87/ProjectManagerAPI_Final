using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using ProjectManager.Data;
using NUnit.Framework;

namespace ProjectManagerAPI.Tests.UnitTests.DataTests
{
    [TestFixture]
    public class UserDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        User user;
        UserData userData;
        public UserDataTest()
        {
            userData = new UserData();
            userData.CreateUser(new User { FirstName = "CheckFname", LastName = "CheckLname", Employee_ID = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();
        }
        [Test]
        public void CreateNewUserDB()
        {
            UserData appRepository = new UserData();
            var result = appRepository.CreateUser(new User { FirstName = "CheckFname", LastName = "CheckLname", Employee_ID = 1 });
            Assert.IsTrue(result);
        }

        [Test]
        public void GetUserDeatilsFromDB()
        {
            var result = userData.GetAllUsers();
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetUserDetailsByID()
        {
            UserData appRepository = new UserData();
            appRepository.CreateUser(new User { FirstName = "CheckFname", LastName = "CheckLname", Employee_ID = 1 });
            user = appRepository._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();
            var result = userData.GetUserByUserId(user.User_ID);
            Assert.NotNull(result);
        }

        [Test]
        public void UpdateUserDetailsByID()
        {
            UserData appRepository = new UserData();
            appRepository.CreateUser(new User { FirstName = "CheckFname", LastName = "CheckLname", Employee_ID = 1 });
            user = appRepository._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();
            var result = userData.UpdateUser(user, user.User_ID);
            Assert.AreEqual(true, result);
        }


        [Test]
        public void DeleteUserDetailsByID()
        {
            UserData appRepository = new UserData();
            appRepository.CreateUser(new User { FirstName = "CheckFname", LastName = "CheckLname", Employee_ID = 1 });
            user = appRepository._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();
            var result = userData.DeleteUser(user.User_ID);
            Assert.AreEqual(true, result);
        }
    }
}
