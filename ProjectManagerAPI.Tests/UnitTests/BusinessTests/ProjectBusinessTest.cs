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
    public class ProjectBusinessTest
    {
        Mock<IProjectData> mock = new Mock<IProjectData>();
        Mock<IUserData> mockuser = new Mock<IUserData>();

        public ProjectBusinessTest()
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
        public void GetAllProjectsFromRepo()
        {
            mock.Setup(a => a.GetAllProjects()).Returns(new List<Project> { new Project { Project_ID = 1, Project1 = "Project", Priority = 1 } });
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockuser.Object);

            List<ProjectDTO> result = appBusiness.GetAllProjects();

            Assert.IsNotNull(result);
            Assert.AreEqual(1,result.Count());
            Assert.AreEqual("Project", result.ElementAt(0).ProjectName);
        }

        [Test]
        public void GetProjectByIdfromrepo()
        {
            mock.Setup(a => a.GetProjectByProjectId(1)).Returns( new Project { Project_ID = 1, Project1 = "Project", Priority = 1  });
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockuser.Object);

            ProjectDTO result = appBusiness.GetProjectByProjectId(1);

            Assert.AreEqual("Project", result.ProjectName);
        }

        [Test]
        public void Updateprojectfromrepo()
        {
            mock.Setup(a => a.UpdateProject(It.IsAny<Project>(),1)).Returns(true);
            mockuser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true); 
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.UpdateProject(new ProjectDTO { Project_Id = 1, ProjectName = "SampleProject", Start_Date = DateTime.Now, End_Date = DateTime.Now }, 1);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void Deleteprojectfromrepo()
        {
            mock.Setup(a => a.DeleteProject(1)).Returns(true);
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.DeleteProject(1);

            Assert.AreEqual(true, result);
        }
    }
}
