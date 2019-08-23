using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Business
{
    public interface ITaskBusiness
    {
        List<TaskDTO> GetTasks();
        TaskDTO GetTaskById(int taskID);
        bool UpdateTask(TaskDTO task, int taskID);
        bool DeleteaskById(int taskId);
        bool EndTaskById(int taskId);
        List<ParentTaskDTO> GetParentTasks();
        bool CreateTask(TaskDTO task);
    }
}
