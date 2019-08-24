using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using ProjectManager.Data;
using AutoMapper;
using NBench;
using System.Web;

[assembly: PreApplicationStartMethod(typeof(ProjectManager.LoadTest.ProjectManagerLoadTest), "Start")]
namespace ProjectManager.LoadTest
{
    public class ProjectManagerLoadTest
    {
        UserData _userData;
        ProjectData _projectData;
        TaskData _appRepository;
        Task task;
        ParentTask parentTask;
        Project project;
        User user;
        public ProjectManagerLoadTest()
        {

        }

        [PerfSetup]
        public void Setup(BenchmarkContext context)
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
            _appRepository = new TaskData();
            _userData = new UserData();
            _projectData = new ProjectData();

            _userData.CreateUser(new User { FirstName = "SampleFName", LastName = "SampleLName", Employee_ID = 1 });
            user = _userData._dbContext.Users.Where(a => a.Employee_ID == 1).FirstOrDefault();

            _projectData.CreateProject(new Project { Project1 = "SampleProject", Priority = 1 });
            project = _projectData._dbContext.Projects.Where(a => a.Project1 == "SampleProject").FirstOrDefault();

            _appRepository.CreateParentTask(new ParentTask { Parent_Task = "SampleParentTask" });
            parentTask = _appRepository._dbContext.ParentTasks.Where(a => a.Parent_Task == "SampleParentTask").FirstOrDefault();

            _appRepository.CreateTask(new Task { Task1 = "SampleTask", Priority = 1, StartDate = DateTime.Now.Date, EndDate = DateTime.Now.Date.AddDays(1), Parent_ID = parentTask.Parent_ID, Project_ID = project.Project_ID });
            task = _appRepository._dbContext.Tasks.Where(a => a.Task1 == "SampleTask").FirstOrDefault();
        }


        [PerfBenchmark(Description = "---------------NBench Result for get_gll_users----------------",
            NumberOfIterations = 2,
            RunMode = RunMode.Throughput,
            TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void get_all_users()
        {
            UserBusiness userBusiness = new UserBusiness(_userData);
            userBusiness.GetAllUsers();
        }

        [PerfBenchmark(Description = "---------------NBench Result for Get_user_By_Id----------------",
          NumberOfIterations = 2,
          RunMode = RunMode.Throughput,
          TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Get_user_By_Id()
        {
            UserBusiness userBusiness = new UserBusiness(_userData);
            userBusiness.GetUserByUserId(1);
        }

        [PerfBenchmark(Description = "---------------NBench Result for Create_a_user----------------",
           NumberOfIterations = 2,
           RunMode = RunMode.Throughput,
           TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Create_a_user()
        {
            UserBusiness userBusiness = new UserBusiness(_userData);
            var result = userBusiness.CreateUser(new UserDTO { FirstName = "Fname", LastName = "Lname", Employee_ID = 1 });
        }


        [PerfBenchmark(Description = "---------------NBench Result for Update_a_user----------------",
        NumberOfIterations = 2,
        RunMode = RunMode.Throughput,
        TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Update_a_user()
        {
            UserBusiness userBusiness = new UserBusiness(_userData);
            var result = userBusiness.UpdateUser(new UserDTO { User_ID = user.User_ID, FirstName = "Fname", LastName = "Lname", Employee_ID = 1 }, 1);
        }



        [PerfBenchmark(Description = "---------------NBench Result for get_all_projects----------------",
      NumberOfIterations = 2,
      RunMode = RunMode.Throughput,
      TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void get_all_projects()
        {
            ProjectBusiness appBusiness = new ProjectBusiness(_projectData, _userData);
            List<ProjectDTO> result = appBusiness.GetAllProjects();
        }


        [PerfBenchmark(Description = "---------------NBench Result for Get_project_By_Id----------------",
 NumberOfIterations = 2,
 RunMode = RunMode.Throughput,
 TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Get_project_By_Id()
        {
            ProjectBusiness appBusiness = new ProjectBusiness(_projectData, _userData);
            ProjectDTO result = appBusiness.GetProjectByProjectId(1);
        }


        [PerfBenchmark(Description = "---------------NBench Result for Create_project----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Create_project()
        {
            ProjectBusiness appBusiness = new ProjectBusiness(_projectData, _userData);
            var result = appBusiness.CreateProject(new ProjectDTO { ProjectName ="SampleProject",Start_Date= DateTime.Now,End_Date =DateTime.Now });
        }


        [PerfBenchmark(Description = "---------------NBench Result for Update_the_project----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void Update_the_project()
        {
            ProjectBusiness appBusiness = new ProjectBusiness(_projectData, _userData);
            var result = appBusiness.UpdateProject(new ProjectDTO { Project_Id=project.Project_ID, ProjectName = "SampleProject", Start_Date = DateTime.Now, End_Date = DateTime.Now },1);
        }


        [PerfBenchmark(Description = "---------------NBench Result for GetAllTasks----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetAllTasks()
        {
            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.GetTasks();
        }


        [PerfBenchmark(Description = "---------------NBench Result for GetAllParentTasks----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetAllParentTasks()
        {
            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.GetParentTasks();
        }


        [PerfBenchmark(Description = "---------------NBench Result for GetTaskById----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void GetTaskById()
        {
            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.GetTaskById(1);
        }


        [PerfBenchmark(Description = "---------------NBench Result for CreateTask----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void CreateTask()
        {

            TaskDTO taskDTO = new TaskDTO()
            {
                Task = "Load Sample Task",
                Priority=1,
                StartDate=DateTime.Now.Date,
                EndDate=DateTime.Now.AddDays(1).Date 
            };

            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.CreateTask(taskDTO);
        }


        [PerfBenchmark(Description = "---------------NBench Result for UpdateTask----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void UpdateTask()
        {

            TaskDTO taskDTO = new TaskDTO()
            {
                Task = "Load Sample Updae Task",
                Priority = 1,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(1).Date,
                TaskId  =  1
            };

            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.UpdateTask(taskDTO,1);
        }


        [PerfBenchmark(Description = "---------------NBench Result for EndTask----------------",
NumberOfIterations = 2,
RunMode = RunMode.Throughput,
TestMode = TestMode.Measurement, SkipWarmups = false)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 1000)]
        public void EndTask()
        {

            TaskBusiness appBusiness = new TaskBusiness(_appRepository, _userData);
            appBusiness.EndTaskById( 1);
        }

        [PerfCleanup]
        public void Cleanup()
        {
        }
    }
}
