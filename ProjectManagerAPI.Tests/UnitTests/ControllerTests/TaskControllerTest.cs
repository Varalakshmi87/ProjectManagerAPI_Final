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

    public class TaskControllerTest
    {
        Mock<ITaskBusiness> mock = new Mock<ITaskBusiness>();
        [Test]
        public void GetAllTask()
        {
            mock.Setup(a => a.GetTasks()).Returns(new List<TaskDTO> { new TaskDTO { TaskId = 1, Task = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date } });
            TaskController controller = new TaskController(mock.Object);

            IEnumerable<TaskDTO> result = controller.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("SampleTask", result.ElementAt(0).Task);
        }

        [Test]
        public void GetAllParentTask()
        {
            mock.Setup(a => a.GetParentTasks()).Returns(new List<ParentTaskDTO> { new ParentTaskDTO { Parent_Id = 1, Parent_Task = "Sample Parent Task" } });
            TaskController controller = new TaskController(mock.Object);

            IEnumerable<ParentTaskDTO> result = controller.GetParents();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Sample Parent Task", result.ElementAt(0).Parent_Task);
        }

        [Test]
        public void GetTaskByID()
        {
            mock.Setup(a => a.GetTaskById(1)).Returns(new TaskDTO { TaskId = 1, Task = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date });
            TaskController controller = new TaskController(mock.Object);

            TaskDTO result = controller.Get(1);

            Assert.AreEqual("SampleTask", result.Task);
        }

        [Test]
        public void CreateTask()
        {


            mock.Setup(a => a.CreateTask(It.IsAny<TaskDTO>())).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            var result = controller.Post(new TaskDTO());

            Assert.AreEqual(true, result);
        }

        [Test]
        public void UpdateTask()
        {

            var task = new TaskDTO { TaskId = 1, Task = "SampleTask", Priority = 1 };
            mock.Setup(a => a.UpdateTask(task, task.TaskId)).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            var result = controller.update(task, task.TaskId);

            Assert.AreEqual(true, result);
        }


        [Test]
        public void EndTask()
        {
            mock.Setup(a => a.EndTaskById(1)).Returns(true);
            TaskController controller = new TaskController(mock.Object);
            var result = controller.End(1);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteTask()
        {
            mock.Setup(a => a.DeleteaskById(1)).Returns(true);
            TaskController controller = new TaskController(mock.Object);
            var result = controller.Delete(1);
            Assert.AreEqual(true, result);
        }

    }
}
