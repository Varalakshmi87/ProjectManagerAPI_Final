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

namespace ProjectManagerAPI.Tests.UnitTests.DataTests
{
    [TestFixture]
    public  class ProjectDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        Project project;
        ProjectData projectData;

        public ProjectDataTest()
        {
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "SampleProject", StartDate =DateTime.Now , Priority  = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();
        }

        [Test]
        public void CreateNewProjectDB()
        {
            ProjectData projectData = new ProjectData();
            var result = projectData.CreateProject(new Project { Project1 = "SampleProject", StartDate = DateTime.Now, Priority = 1 });
            Assert.IsTrue(result>0);
        }

        [Test]
        public void GetAllProjectDeailsDB()
        {
            ProjectData projectData = new ProjectData();
            var result = projectData.GetAllProjects();
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetProjectDeailsByID()
        {
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "SampleProject", StartDate = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();
            var result = projectData.GetProjectByProjectId(project.Project_ID);
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateProjectDeailsByID()
        {
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "SampleProject", StartDate = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();
            var result = projectData.UpdateProject(project ,project.Project_ID);
            Assert.AreEqual(true,result);
        }
        [Test]
        public void DeleteProjectDeailsByID()
        {
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "SampleProject", StartDate = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();
            var result = projectData.DeleteProject(project.Project_ID);
            Assert.AreEqual(true , result);
        }
    }
}
