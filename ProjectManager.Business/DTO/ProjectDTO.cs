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
        public int Project_ID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int Priority { get; set; }
        public int? UserId { get; set; }
        public List<UserDTO> lstUser { get; set; }
        public int TotalTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }




    }
}
