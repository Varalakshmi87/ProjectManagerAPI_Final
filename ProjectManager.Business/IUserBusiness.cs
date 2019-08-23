using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Business.DTO;


namespace ProjectManager.Business
{
    public interface IUserBusiness
    {
        List<UserDTO> GetAllUsers();
        UserDTO GetUserByUserId(int userid);
        bool CreateUser(UserDTO user);
        bool UpdateUser(UserDTO user, int userid);
        bool DeleteUser(int userid);
    }
}
