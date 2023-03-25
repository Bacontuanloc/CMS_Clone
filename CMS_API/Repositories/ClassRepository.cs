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

        public List<Class> FindClassByClassCode(string code) => ClassDAO.FindClassByClassCode(code);

        public Class FindClassById(int id) => ClassDAO.FindClassById(id);

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
