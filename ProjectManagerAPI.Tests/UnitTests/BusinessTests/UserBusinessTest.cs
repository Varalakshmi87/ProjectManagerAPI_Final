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


namespace ProjectManagerAPI.Tests.UnitTests.BusinessTests
{
    [TestFixture]
    public class UserBusinessTest
    {
        Mock<IUserData> mockuser = new Mock<IUserData>();

        public UserBusinessTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskDTO, Task>()
                .ForMember(dest => dest.Task1, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.Task_ID, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_ID, opt => opt.MapFrom(src => src.Project_Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_ID, opt => opt.MapFrom(src => src.Parent_Id));

                cfg.CreateMap<ParentTaskDTO, ParentTask>()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.Task1))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task_ID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_Id, opt => opt.MapFrom(src => src.Project_ID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_Id, opt => opt.MapFrom(src => src.Parent_ID))
                .ForMember(dest => dest.ProjectDTOName, opt => opt.MapFrom(src => src.Project.Project1))
                .ForMember(dest => dest.ParentDTOName, opt => opt.MapFrom(src => src.ParentTask.Parent_Task));

                cfg.CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.Project1, opt => opt.MapFrom(src => src.ProjectName));
                cfg.CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project1))
                .ForMember(dest => dest.TotalTaskCount, opt => opt.MapFrom(src => src.Tasks.Count))
                .ForMember(dest => dest.lstUser, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.CompletedTaskCount, opt => opt.MapFrom(src => src.Tasks.Where(a => a.Status.ToUpper().Trim() == "COMPLETED").Count()));

            });
        }
        [Test]
        public void GetUserFromrepo()
        {
            mockuser.Setup(a => a.GetAllUsers()).Returns(new List<User> { new User { FirstName = "Fname", LastName = "Lname", Employee_ID = 1 } });
            UserBusiness appBusiness = new UserBusiness(mockuser.Object);

            List<UserDTO> result = appBusiness.GetAllUsers();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Fname", result.ElementAt(0).FirstName);
        }

        [Test]
        public void GetUserbyIdFromrepo()
        {
            mockuser.Setup(a => a.GetUserByUserId(1)).Returns(new User { User_ID = 1, FirstName = "Fname", LastName = "Lname", Employee_ID = 1 });
            UserBusiness appBusiness = new UserBusiness(mockuser.Object);

            UserDTO result = appBusiness.GetUserByUserId(1);
            Assert.AreEqual("Fname", result.FirstName);
        }

        [Test]
        public void CreateUserFromRepo()
        {
            mockuser.Setup(a => a.CreateUser(It.IsAny<User>())).Returns(true);
            UserBusiness appBusiness = new UserBusiness(mockuser.Object);

            var result = appBusiness.CreateUser(new UserDTO());
            Assert.AreEqual(true, result);
        }
        [Test]
        public void UpdateUserFromRepo()
        {
            mockuser.Setup(a => a.UpdateUser(It.IsAny<User>(), 1)).Returns(true);
            mockuser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            UserBusiness appBusiness = new UserBusiness(mockuser.Object);

            var result = appBusiness.UpdateUser(new UserDTO { User_ID = 1, FirstName = "Fname", LastName = "Lname", Employee_ID = 1 }, 1);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteUserFromRepo()
        {
            mockuser.Setup(a => a.DeleteUser(1)).Returns(true);
            UserBusiness appBusiness = new UserBusiness(mockuser.Object);

            var result = appBusiness.DeleteUser(1);
            Assert.AreEqual(true, result);
        }

    }
}
