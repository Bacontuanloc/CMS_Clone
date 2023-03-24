using CMS_API.DAO;
using CMS_API.Entities;
using CMS_API.Repositories.Interfaces;

namespace CMS_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers() => UserDAO.GetUsers();
        public User GetUser(string username, string password) => UserDAO.GetUser(username, password);
    }
}
