using CMS_API.Entities;

namespace CMS_API.Repositories.Interfaces
{
    public interface IClassRepository
    {
        List<Class> GetClasses();
        Class FindClassByClassCode(string code);
        Class FindClassById(int id);
        Class FindClassByUserClassId(int id);
        void SaveClass(Class c);
        void UpdateClass(Class c);
        void DeleteClass(Class c);
        List<Class> FindClassByUserId(int id);
    }
}
