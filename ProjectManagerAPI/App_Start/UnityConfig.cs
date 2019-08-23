using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unity;
using ProjectManager.Business;
using ProjectManager.Data;

namespace ProjectManagerAPI
{
    public class UnityConfig
    {
        public static void RegisterContainers(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<ITaskData, TaskData>();
            container.RegisterType<IUserData, UserData>();
            container.RegisterType<IProjectBusiness, ProjectBusiness>();
            container.RegisterType<ITaskBusiness, TaskBusiness>();
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.Resolve<ProjectBusiness>();
            container.Resolve<TaskBusiness>();
            container.Resolve<UserBusiness>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}