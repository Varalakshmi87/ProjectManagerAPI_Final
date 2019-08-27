using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagerAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        IUserBusiness _userBusiness;

        public UserController()
        {

        }

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }
        public IEnumerable<UserDTO> Get()
        {
            return _userBusiness.GetAllUsers();
        }

        public UserDTO Get(int id)
        {
            return _userBusiness.GetUserByUserId(id);
        }

        public bool Post([FromBody]UserDTO value)
        {
            return _userBusiness.CreateUser(value);
        }
        public bool Put(int id, [FromBody]UserDTO value)
        {
            return _userBusiness.UpdateUser(value, id);
        }
        public bool Delete(int id)
        {
            return _userBusiness.DeleteUser(id);
        }
    }
}