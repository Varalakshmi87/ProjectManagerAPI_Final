using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Business.DTO;

namespace ProjectManager.Business
{
   public  class ProjectDTO
    {
        public int Project_Id { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public int Priority { get; set; }
        public int? UserId { get; set; }
        public List<UserDTO> lstUser { get; set; }
        public int TotalTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }




    }
}
