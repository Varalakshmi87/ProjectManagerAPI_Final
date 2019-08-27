﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using ProjectManager.Business;
using ProjectManager.Data;
using ProjectManager.Business.DTO;
using System.Web.Http;

namespace ProjectManagerAPI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(UnityConfig.RegisterContainers);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskDTO, Task>()
                .ForMember(dest => dest.Task1, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.Task_ID, opt => opt.MapFrom(src => src.TaskID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_ID, opt => opt.MapFrom(src => src.Project_ID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_ID, opt => opt.MapFrom(src => src.Parent_ID));

                cfg.CreateMap<ParentTaskDTO, ParentTask>()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());

                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.Task1))
                .ForMember(dest => dest.TaskID, opt => opt.MapFrom(src => src.Task_ID))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_ID, opt => opt.MapFrom(src => src.Project_ID))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_ID, opt => opt.MapFrom(src => src.Parent_ID))
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
    }
}
