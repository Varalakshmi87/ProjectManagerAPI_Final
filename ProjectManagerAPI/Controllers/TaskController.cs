using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManager.Business;
using System.Web.Http.Cors;

namespace ProjectManagerAPI.Controllers
{
    [RoutePrefix("Task")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        ITaskBusiness _taskBusiness;

        public TaskController(ITaskBusiness taskBusiness)
        {
            _taskBusiness = taskBusiness;
        }
        [Route("get/{id}")]
        public IEnumerable<TaskDTO> GetAll()
        {
            return _taskBusiness.GetTasks();
        }
        [Route("get/{id}")]
        public TaskDTO Get(int id)
        {
            return _taskBusiness.GetTaskById(id);
        }
        [Route("create")]
        public bool Post([FromBody]TaskDTO value)
        {
            return _taskBusiness.CreateTask(value);
        }
        [Route("update/{id}")]
        [HttpPost]
        public bool update([FromBody]TaskDTO value, int id)
        {
            return _taskBusiness.UpdateTask(value, id);
        }
        [Route("delete/{id}")]
        public bool Delete(int id)
        {
            return _taskBusiness.DeleteaskById(id);
        }
        [Route("end/{id}")]
        [HttpGet]
        public bool End(int id)
        {
            return _taskBusiness.EndTaskById( id);
        }
        [Route("getParents")]
        [HttpGet]
        public IEnumerable<ParentTaskDTO> GetParents()
        {
            return _taskBusiness.GetParentTasks();
        }
    }
}
