using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public class TaskData : ITaskData
    {
        public ProjectManagerEntities _dbContext;
        public TaskData()
        {
            _dbContext = new ProjectManagerEntities();
        }

        public bool CreateParentTask(ParentTask parentTask)
        {
            try
            {
                _dbContext.ParentTasks.Add(parentTask);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        

        public bool DeleteaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_ID == taskId).FirstOrDefault();
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool EndTaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_ID == taskId).FirstOrDefault();
                task.EndDate = DateTime.Now.Date;
                task.Status = "COMPLETED";
                _dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }


        public int CreateTask(Task task)
        {
            int createdTask = 0;
            try
            {
                task.Task_ID = 0;                
                task.Status = "SARTED";
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                createdTask = task.Task_ID;
            }
            catch (Exception ex)
            {
                createdTask=0;
            }
            return createdTask;
        }

        public List<ParentTask > GetParentTasks()
        {
            List<ParentTask> tasks  = new List<ParentTask>();
            try
            {
                tasks = _dbContext.ParentTasks.ToList();

            }
            catch (Exception ex)
            {
            }
            return tasks;
        }
        public Task GetTaskById(int taskid)
        {
            Task  task = new Task();
            try
            {
                task = _dbContext.Tasks.Include("ParentTask").Include("Project").Include("Users").Where(a=>a.Task_ID ==taskid).FirstOrDefault();

            }
            catch (Exception ex)
            {
            }
            return task;
        }
        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                tasks = _dbContext.Tasks.Include("ParentTask").Include("Project").Include("Users").ToList();

            }
            catch (Exception ex)
            {
            }
            return tasks;
        }
        public bool UpdateTask(Task task, int taskID)
        {
            bool result = false;
            try
            {
                Task  taskFromDB = _dbContext.Tasks.Where(a => a.Task_ID  == taskID).FirstOrDefault();
                taskFromDB.Task1  = task.Task1;
                taskFromDB.Status  = task.Status;
                taskFromDB.Priority = task.Priority;
                taskFromDB.Project_ID = task.Project_ID;
                taskFromDB.Parent_ID = task.Parent_ID;
                taskFromDB.StartDate = task.StartDate;
                taskFromDB.EndDate = taskFromDB.EndDate;
                _dbContext.Entry(taskFromDB).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
