using CMS_API.Entities;
using CMS_API.Models;

namespace CMS_API.Repositories.Interfaces
{
    public interface IUserClassRepository
    {
        List<UserClass> GetUserClass();
        UserClass FindUserClassById(int id);
        List<UserClass> FindUserClassByClassId(int classId);
        void SaveUserClass(UserClassModel c);
        void UpdateUserClass(UserClassModel c);
        void DeleteUserClass(UserClass c);
    }
}
