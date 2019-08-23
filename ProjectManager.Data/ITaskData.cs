using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public interface ITaskData
    {
        List<Task> GetTasks();
        Task GetTaskById(int taskID);
        bool UpdateTask(Task task, int taskID);
        bool DeleteaskById(int taskId);
        bool EndTaskById(int taskId);
        List<ParentTask> GetParentTasks();
        int CreateTask(Task task);
        bool CreateParentTask(ParentTask parentTask);
    }
}
