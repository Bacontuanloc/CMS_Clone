using CMS_API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CMS_API.DAO
{
    public class ClassDAO
    {
        public static List<Class> GetClasses()
        {
            var listClasses = new List<Class>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listClasses = context.Classes.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listClasses;
        }
        public static Class FindClassById(int id)
        {
            Class c = new Class();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    c = context.Classes.Where(c => c.ClassId == id).SingleOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return c;
        }
        public static List<Class> FindClassByClassCode(string code)
        {
            var listClasses = new List<Class>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listClasses = context.Classes.Where(c => c.ClassCode.ToLower().Equals(code.ToLower())).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listClasses;
        }
        public static List<Class> FindClassByStudentId(int id)
        {
            var listClasses = new List<Class>();
            var listUserClasses = new List<int>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listUserClasses = context.UserClasses.Include(uc => uc.User).ThenInclude(u => u.Role).Where(u => u.UserId == id && u.User.Role.RoleName.Equals("Student")).Select(u => u.ClassId).ToList();
                    listClasses = (List<Class>)context.Classes.Include(c => listUserClasses.Contains(c.ClassId));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listClasses;
        }
        public static void SaveClass(Class c)
        {
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    context.Classes.Add(c);
                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateClass(Class c)
        {
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    context.Entry<Class>(c).State =
                        EntityState.Modified;
                    context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void DeleteClass(Class c)
        {
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    var c1 = context.Classes.SingleOrDefault(c => c.ClassId == c.ClassId);
                    context.Classes.Remove(c1);
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
