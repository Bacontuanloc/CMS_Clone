using CMS_API.DAO;
using CMS_API.Entities;
using CMS_API.Models;
using CMS_API.Repositories.Interfaces;

namespace CMS_API.Repositories
{
    public class UserClassRepository : IUserClassRepository
    {
        public void DeleteUserClass(UserClass c)
        {
            UserClassDAO.DeleteUserClass(c);
        }

        public List<UserClass> FindUserClassByClassId(int classId)
        {
            return UserClassDAO.FindUserClassByClassId(classId);
        }

        public UserClass FindUserClassById(int id)
        {
            return UserClassDAO.FindUserClassById(id);
        }

        public List<UserClass> FindUserClassByUserId(int userId)
        {
            return UserClassDAO.FindUserClassByUserId(userId);
        }

        public List<UserClass> GetUserClass()
        {
            return UserClassDAO.GetUserClass();
        }

        public void SaveUserClass(UserClassModel c)
        {
            UserClassDAO.SaveUserClass(c);
        }

        public void UpdateUserClass(UserClassModel c)
        {
            UserClassDAO.UpdateUserClass(c);
        }
    }
}
