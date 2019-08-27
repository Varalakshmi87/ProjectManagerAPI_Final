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
   public class ProjectControllerTest
    {
        Mock<IProjectBusiness> mock = new Mock<IProjectBusiness>();

        [Test]
        public void GetProjects()
        {
            mock.Setup(a => a.GetAllProjects()).Returns(new List<ProjectDTO> { new ProjectDTO { Project_ID = 1, ProjectName = "SampleProject", Priority = 1,StartDate=DateTime.Now } });
            ProjectController controller = new ProjectController(mock.Object);

            IEnumerable<ProjectDTO> result = controller.Get();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("SampleProject", result.ElementAt(0).ProjectName);
        }

        [Test]
        public void GetProjectById()
        {
            mock.Setup(a => a.GetProjectByProjectId(1)).Returns( new ProjectDTO { Project_ID = 1, ProjectName = "SampleProject", Priority = 1, StartDate = DateTime.Now });
            ProjectController controller = new ProjectController(mock.Object);

            var result = controller.Get(1);

            Assert.IsNotNull(result);
        }

        [Test]
        public void CreateNewProject()
        {
            var dto = new ProjectDTO { Project_ID = 1, ProjectName = "SampleProject", Priority = 1, StartDate = DateTime.Now };
            mock.Setup(a => a.CreateProject(dto)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            var result = controller.Post(dto);

            Assert.AreEqual(true,result);
        }


        [Test]
        public void UpdateExistingProject()
        {
            var dto = new ProjectDTO { Project_ID = 1, ProjectName = "SampleProject", Priority = 1, StartDate = DateTime.Now };
            mock.Setup(a => a.UpdateProject(dto,dto.Project_ID)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            var result = controller.Put(dto.Project_ID,dto);

            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteExistingProject()
        {
            var dto = new ProjectDTO { Project_ID = 1, ProjectName = "SampleProject", Priority = 1, StartDate = DateTime.Now };
            mock.Setup(a => a.DeleteProject(1)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            var result = controller.Delete(1);

            Assert.AreEqual(true, result);
        }
    }
}
