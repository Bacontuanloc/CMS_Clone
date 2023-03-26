using CMS_API.Entities;
using CMS_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CMS_API.DAO
{
    public class UserClassDAO
    {
        public static List<UserClass> GetUserClass()
        {
            var listUserClass = new List<UserClass>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listUserClass = context.UserClasses.Include(c => c.Class).Include(u => u.User).Where(uc => uc.User.RoleId == 2).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listUserClass;
        }
        public static UserClass FindUserClassById(int id)
        {
            UserClass c = new UserClass();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    c = context.UserClasses.Include(u => u.User).Include(c => c.Class).Where(c => c.Id == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return c;
        }
        public static void SaveUserClass(UserClassModel c)
        {
            UserClass uc = new UserClass();
            uc.Id = 0;
            uc.ClassId = c.ClassId;
            uc.UserId = c.UserId;
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    uc.User = context.Users.Where(u => u.UserId == c.UserId).FirstOrDefault();
                    uc.Class = context.Classes.Where(cl => cl.ClassId == c.ClassId).FirstOrDefault();
                    context.UserClasses.Add(uc);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateUserClass(UserClassModel c)
        {
            UserClass uc = new UserClass();
            uc.Id = c.Id;
            uc.ClassId = c.ClassId;
            uc.UserId = c.UserId;
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    uc.User = context.Users.Where(u => u.UserId == c.UserId).FirstOrDefault();
                    uc.Class = context.Classes.Where(cl => cl.ClassId == c.ClassId).FirstOrDefault();
                    //context.Entry<UserClass>(uc).State =
                    //    EntityState.Modified;
                    context.UserClasses.Update(uc);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteUserClass(UserClass c)
        {
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    var c1 = context.UserClasses.Where(uc => uc.Id == c.Id).FirstOrDefault();
                    context.UserClasses.Remove(c1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
