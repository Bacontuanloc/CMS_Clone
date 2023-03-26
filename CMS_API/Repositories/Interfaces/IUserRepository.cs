using CMS_API.Entities;

namespace CMS_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUser(string username,string password);
        List<User> GetAllTeacher();
        User FindUserByUserId(int id);
    }
}
