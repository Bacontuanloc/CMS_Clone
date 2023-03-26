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
        public static Class FindClassByClassCode(string code)
        {
            Class c = new Class();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    c = context.Classes.Where(c => c.ClassCode.ToLower().Equals(code.ToLower())).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return c;
        }
        public static List<Class> FindClassByUserId(int id)
        {
            var listClasses = new List<Class>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listClasses = context.UserClasses.Include(uc => uc.User).Include(c => c.Class).Where(uc => uc.UserId == id).Select(uc => uc.Class).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listClasses;
        }
        public static Class FindClassByUserClassId(int id)
        {
            Class c = new Class();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    c = context.UserClasses.Where(uc => uc.Id == id).Select(c => c.Class).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return c;
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
                    var c1 = context.Classes.Where(cl => cl.ClassId == c.ClassId).FirstOrDefault();
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
