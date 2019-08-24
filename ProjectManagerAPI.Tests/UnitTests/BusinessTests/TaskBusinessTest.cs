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
    public class TaskBusinessTest
    {
        Mock<ITaskData> mock = new Mock<ITaskData>();
        Mock<IUserData> mockuser = new Mock<IUserData>();
        public TaskBusinessTest()
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
        public void get_all_task_from_repo()
        {
            mock.Setup(a => a.GetTasks()).Returns(new List<Task> { new Task { Task_ID = 1, Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date } });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            List<TaskDTO> result = appBusiness.GetTasks();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("SampleTask", result.ElementAt(0).Task);
        }

        [Test]
        public void Get_Task_By_Id_from_repo()
        {
            mock.Setup(a => a.GetTaskById(1)).Returns(new Task { Task_ID = 1, Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            TaskDTO result = appBusiness.GetTaskById(1);

            Assert.AreEqual("SampleTask", result.Task);
        }

        [Test]
        public void get_all_parentTask_from_repo()
        {
            mock.Setup(a => a.GetParentTasks()).Returns(new List<ParentTask> { new ParentTask { Parent_ID = 1, Parent_Task = "Sample Parent Task" } });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            IEnumerable<ParentTaskDTO> result = appBusiness.GetParentTasks();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Sample Parent Task", result.ElementAt(0).Parent_Task);
        }

        [Test]
        public void CreateTaskfromrepo()
        {
            mock.Setup(a => a.CreateTask(It.IsAny<Task>())).Returns(1);
            mockuser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.CreateTask(new TaskDTO());

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CreateParentTaskfromrepo()
        {
            mock.Setup(a => a.CreateParentTask(It.IsAny<ParentTask>())).Returns(true);
            mockuser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            var task = new TaskDTO();
            task.IsParentTask = true;
            var result = appBusiness.CreateTask(task);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateTaskfromrepo()
        {
            mock.Setup(a => a.UpdateTask(It.IsAny<Task>(), 1)).Returns(true);
            mockuser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.UpdateTask(new TaskDTO { TaskId = 1, Task = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date }, 1);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void EndTaskFromRepo()
        {
            mock.Setup(a => a.EndTaskById(1)).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.EndTaskById(1);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteTaskFromRepo()
        {
            mock.Setup(a => a.DeleteaskById(1)).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockuser.Object);

            var result = appBusiness.DeleteaskById(1);

            Assert.AreEqual(true, result);
        }
    }
}
