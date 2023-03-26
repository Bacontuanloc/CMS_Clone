using CMS_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS_API.DAO
{
    public class UserDAO
    {
        public static List<User> GetUsers()
        {
            var listUsers = new List<User>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listUsers = context.Users.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listUsers;
        }

        public static User GetUser(string username,string password)
        {
            var user = new User();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    user = context.Users.Include(c=>c.Role).Where(c=>c.Username.Equals(username)&&c.Password.Equals(password)).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return user;
        }

        public static List<User> GetAllTeacher()
        {
            var listTeachers = new List<User>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listTeachers = context.Users.Where(u => u.RoleId == 2).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listTeachers;
        }
        public static User FindUserByUserId(int id)
        {
            User u = new User();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    u = context.Users.Where(u => u.UserId == id).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return u;
        }
    }
}
