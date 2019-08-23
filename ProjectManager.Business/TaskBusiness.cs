using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ProjectManager.Data;

namespace ProjectManager.Business
{
    public class TaskBusiness : ITaskBusiness
    {
        ITaskData _taskData;
        IUserData _userData;
        public TaskBusiness(ITaskData taskData, IUserData userData)
        {
            _taskData = taskData;
            _userData = userData;
        }
        public bool CreateTask(TaskDTO task)
        {
            bool result = false;
            try
            {
                Data.Task taskDb = Mapper.Map<Data.Task>(task);
                if (task.IsParentTask)
                {
                    ParentTask parentTask = new ParentTask();
                    parentTask.Parent_Task = task.Task;
                    parentTask.Parent_ID = 0;
                    result = _taskData.CreateParentTask(parentTask);
                }
                else
                {
                    taskDb.ParentTask = null;
                    taskDb.Users = null;
                    taskDb.Project = null;
                    int generateTaskId = _taskData.CreateTask(taskDb);
                    if (generateTaskId > 0)
                    {
                        if (task.UserId != null && task.UserId > 0)
                        {
                            return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(task.UserId), task.Project_Id, generateTaskId);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return result;
        }

        public bool DeleteaskById(int taskId)
        {
            return _taskData.DeleteaskById(taskId);
        }
        public bool EndTaskById(int taskId)
        {
            return _taskData.EndTaskById(taskId);
        }

        public List<TaskDTO> GetTasks()
        {
            List<Data.Task> tasks = _taskData.GetTasks();
            List<TaskDTO> DTOTasks = Mapper.Map<List<TaskDTO>>(tasks);
            return DTOTasks;
        }

        public TaskDTO GetTaskById(int taskID)
        {
            Data.Task tasks = _taskData.GetTaskById(taskID);
            TaskDTO DTOTasks = Mapper.Map<TaskDTO>(tasks);
            return DTOTasks;

        }

        public bool UpdateTask(TaskDTO task, int taskID)
        {
            bool result = false;
            try
            {
                Task taskDb = Mapper.Map<Task>(task);
                taskDb.ParentTask = null;
                taskDb.Users = null;
                taskDb.Project = null;
                bool udatedTask = _taskData.UpdateTask(taskDb,taskID);
                if (udatedTask)
                {
                    if (task.UserId != null && task.UserId > 0)
                    {
                        return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(task.UserId), task.Project_Id, task.TaskId);
                    }
                    return true;
                }
                else
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<ParentTaskDTO> GetParentTasks()
        {
            List<ParentTask> parentTasks = _taskData.GetParentTasks();
            List<ParentTaskDTO> parentDTOTasks = Mapper.Map<List<ParentTaskDTO>>(parentTasks);
            return parentDTOTasks;
        }

    }
}
