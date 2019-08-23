using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;

namespace ProjectManager.Data
{
    public class UserData : IUserData
    {
        private static SqlProviderServices instance = SqlProviderServices.Instance;
        public ProjectManagerEntities _dbContext;
        public UserData()
        {
            _dbContext = new ProjectManagerEntities();
        }

        public bool CreateUser(User user)
        {
            bool result = false;
            try
            {
                user.User_ID = 0;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteUser(int userid)
        {
            bool result = false;
            try
            {
                User user = _dbContext.Users.Where(a => a.User_ID == userid).FirstOrDefault();
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> lstusers = new List<User>();
            try
            {
                lstusers = _dbContext.Users.ToList();

            }
            catch (Exception ex)
            {
            }
            return lstusers;
        }
        public User GetUserByUserId(int userid)
        {
            User user = new User();
            try
            {
                user = _dbContext.Users.Where(a => a.User_ID == userid).FirstOrDefault();

            }
            catch (Exception ex)
            {
            }
            return user;
        }
        public bool UpdateUser(User user, int userid)
        {
            bool result = false;
            try
            {
                User userFromDB = _dbContext.Users.Where(a => a.User_ID == userid).FirstOrDefault();
                userFromDB.Employee_ID = user.Employee_ID;
                userFromDB.FirstName = user.FirstName;
                userFromDB.LastName = user.LastName;
                userFromDB.Project_ID = user.Project_ID;
                userFromDB.Task_ID = user.Task_ID;
                _dbContext.Entry(userFromDB).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool UpdateUserProjectIdTaskId(int userid, int? projectId, int? taskId)
        {
            bool result = false;
            try
            {
                if (projectId != null && projectId > 0)
                {
                    List<User> lstUsers = _dbContext.Users.Where(a => a.Project_ID == projectId).ToList();
                    foreach (var item in lstUsers)
                    {
                        item.Project_ID = null;
                        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
                if (taskId  != null && taskId > 0)
                {
                    List<User> lstUsers = _dbContext.Users.Where(a => a.Task_ID == taskId).ToList();
                    foreach (var item in lstUsers)
                    {
                        item.Task_ID  = null;
                        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
                User userFromDB = _dbContext.Users.Where(a => a.User_ID == userid).FirstOrDefault();                
                userFromDB.Project_ID = projectId;
                userFromDB.Task_ID = taskId;
                _dbContext.Entry(userFromDB).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

    }
}
