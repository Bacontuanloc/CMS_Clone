using CMS_API.Entities;

namespace CMS_API.Repositories.Interfaces
{
    public interface IClassRepository
    {
        List<Class> GetClasses();
        List<Class> FindClassByClassCode(string code);
        Class FindClassById(int id);
        void SaveClass(Class c);
        void UpdateClass(Class c);
        void DeleteClass(Class c);
        List<Class> FindClassByStudentId(int id);
        List<Class> FindClassByTeacherId(int id);
    }
}
