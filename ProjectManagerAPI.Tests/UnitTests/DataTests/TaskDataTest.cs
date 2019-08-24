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
    public class TaskDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        Task task;
        ParentTask parentTask;
        Project project;
        User user;

        public TaskDataTest()
        {
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            ProjectData projectdata = new ProjectData();
            userData.CreateUser(new User { FirstName ="SampleFName",LastName ="SampleLName",Employee_ID = 1});
            user = userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();

            projectdata.CreateProject(new Project { Project1 ="SampleProject",Priority = 1});
            project = projectdata._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();

            taskData.CreateParentTask(new ParentTask { Parent_Task  = "SampleParentTask" });
            parentTask = taskData._dbContext.ParentTasks.Where(a => a.Parent_Task == "SampleParentTask").FirstOrDefault();

            taskData.CreateTask(new Task() { Task1 ="SampleTask",Priority=1,StartDate = DateTime.Now.Date,EndDate=DateTime.Now.Date.AddDays(1),Parent_ID =parentTask.Parent_ID ,Project_ID =project.Project_ID  });
            task = taskData._dbContext.Tasks.Where(a => a.Task1 == "SampleTask").FirstOrDefault();
        }

   
        [Test]
        public void CreateTaskInDB()
        {
            
            TaskData taskData = new TaskData();

            var result = taskData.GetTasks();

            Assert.IsNotNull(result);
        }

        [Test]
        public void FetchTaskFromDB()
        {
            
            TaskData taskData = new TaskData();
            var result = taskData.CreateTask(new Task() { Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(1) });
            Assert.NotZero(result);
        }

        [Test]
        public void FetchParentTaskFromDB()
        {

            TaskData taskData = new TaskData();
            var result = taskData.GetParentTasks();
            Assert.IsNotNull(result);
        }

        [Test]
        public void FetchTaskFromDBById()
        {

            TaskData taskData = new TaskData();
            var result = taskData.GetTaskById(task.Task_ID);
            Assert.NotNull(result);
        }

        [Test]
        public void UpdateTaskInDB()
        {
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            ProjectData projectdata = new ProjectData();
            userData.CreateUser(new User { FirstName = "SampleFName", LastName = "SampleLName", Employee_ID = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();

            projectdata.CreateProject(new Project { Project1 = "SampleProject", Priority = 1 });
            project = projectdata._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();

            taskData.CreateParentTask(new ParentTask { Parent_Task = "SampleParentTask" });
            parentTask = taskData._dbContext.ParentTasks.Where(a => a.Parent_Task == "SampleParentTask").FirstOrDefault();

            taskData.CreateTask(new Task() { Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(1), Parent_ID = parentTask.Parent_ID, Project_ID = project.Project_ID });
            task = taskData._dbContext.Tasks.Where(a => a.Task1 == "SampleTask").FirstOrDefault();

            var result = taskData.UpdateTask(task,task.Task_ID);
            Assert.AreEqual(true,result);
        }

        [Test]
        public void EndTaskInDB()
        {
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            ProjectData projectdata = new ProjectData();
            userData.CreateUser(new User { FirstName = "SampleFName", LastName = "SampleLName", Employee_ID = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();

            projectdata.CreateProject(new Project { Project1 = "SampleProject", Priority = 1 });
            project = projectdata._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();

            taskData.CreateParentTask(new ParentTask { Parent_Task = "SampleParentTask" });
            parentTask = taskData._dbContext.ParentTasks.Where(a => a.Parent_Task == "SampleParentTask").FirstOrDefault();

            taskData.CreateTask(new Task() { Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(1), Parent_ID = parentTask.Parent_ID, Project_ID = project.Project_ID });
            task = taskData._dbContext.Tasks.Where(a => a.Task1 == "SampleTask").FirstOrDefault();

            var result = taskData.EndTaskById(task.Task_ID);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DeleteTaskInDB()
        {
            TaskData taskData = new TaskData();
            UserData userData = new UserData();
            ProjectData projectdata = new ProjectData();
            userData.CreateUser(new User { FirstName = "SampleFName", LastName = "SampleLName", Employee_ID = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();

            projectdata.CreateProject(new Project { Project1 = "SampleProject", Priority = 1 });
            project = projectdata._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();

            taskData.CreateParentTask(new ParentTask { Parent_Task = "SampleParentTask" });
            parentTask = taskData._dbContext.ParentTasks.Where(a => a.Parent_Task == "SampleParentTask").FirstOrDefault();

            taskData.CreateTask(new Task() { Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(1), Parent_ID = parentTask.Parent_ID, Project_ID = project.Project_ID });
            task = taskData._dbContext.Tasks.Where(a => a.Task1 == "SampleTask").FirstOrDefault();

            var result = taskData.DeleteaskById(task.Task_ID);
            Assert.AreEqual(true, result);
        }
    }
}
