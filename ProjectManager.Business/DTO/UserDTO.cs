using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Business.DTO
{
    public class UserDTO
    {
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Employee_ID { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public Nullable<int> Task_ID { get; set; }
    }
}
