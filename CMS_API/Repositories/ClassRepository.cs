using CMS_API.DAO;
using CMS_API.Entities;
using CMS_API.Repositories.Interfaces;

namespace CMS_API.Repositories
{
    public class ClassRepository : IClassRepository
    {
        public void DeleteClass(Class c)
        {
            ClassDAO.DeleteClass(c);
        }

        public Class FindClassByClassCode(string code) => ClassDAO.FindClassByClassCode(code);

        public Class FindClassById(int id) => ClassDAO.FindClassById(id);

        public Class FindClassByUserClassId(int id)
        {
            return ClassDAO.FindClassByUserClassId(id);
        }

        public List<Class> FindClassByUserId(int id) => ClassDAO.FindClassByUserId(id);

        public List<Class> GetClasses() => ClassDAO.GetClasses();

        public void SaveClass(Class c)
        {
            ClassDAO.SaveClass(c);
        }

        public void UpdateClass(Class c)
        {
            ClassDAO.UpdateClass(c);
        }
    }
}
