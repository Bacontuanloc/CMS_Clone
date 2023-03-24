using CMS_API.DAO;
using CMS_API.Entities;
using CMS_API.Repositories.Interfaces;

namespace CMS_API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public List<Role> GetRoles() => RoleDAO.GetRoles();
    }
}
