using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using AutoMapper;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using ProjectManager.Data;
using NUnit.Framework;
using ProjectManagerAPI.Controllers;

namespace ProjectManagerAPI.Tests.UnitTests.ControllerTests
{
    [TestFixture]
    public class UserControllerTest
    {
        Mock<IUserBusiness> mockuser = new Mock<IUserBusiness>();
        [Test]
        public void GetAllUsers()
        {
            mockuser.Setup(a => a.GetAllUsers()).Returns(new List<UserDTO> { new UserDTO { FirstName = "Fname", LastName = "Lname", Employee_ID = 1 } });
            UserController userController = new UserController(mockuser.Object);

            IEnumerable<UserDTO> result = userController.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Fname", result.ElementAt(0).FirstName);
        }
        [Test]
        public void GetUserByID()
        {
            mockuser.Setup(a => a.GetUserByUserId(1)).Returns( new UserDTO { FirstName = "Fname", LastName = "Lname", Employee_ID = 1 } );
            UserController userController = new UserController(mockuser.Object);

            UserDTO result = userController.Get(1);

            Assert.AreEqual("Fname", result.FirstName);
        }

        [Test]
        public void UpdateTask()
        {

            var task = new UserDTO {  User_ID = 1, FirstName = "Fname", Employee_ID = 1 };
            mockuser.Setup(a => a.UpdateUser(task,task.User_ID )).Returns(true);
            UserController userController = new UserController(mockuser.Object);

            var result = userController.Put(task.User_ID,task);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateUser()
        {

            
            mockuser.Setup(a => a.CreateUser(It.IsAny<UserDTO>())).Returns(true);
            UserController userController = new UserController(mockuser.Object);

            var result = userController.Post(new UserDTO ());

            Assert.AreEqual(true, result);
        }

        
        [Test]
        public void DeleteTask()
        {            
            mockuser.Setup(a => a.DeleteUser(1)).Returns(true);
            UserController userController = new UserController(mockuser.Object);

            var result = userController.Delete(1);

            Assert.AreEqual(true, result);
        }
    }
}
