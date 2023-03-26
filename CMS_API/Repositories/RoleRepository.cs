using CMS_API.DAO;
using CMS_API.Entities;
using CMS_API.Repositories.Interfaces;

namespace CMS_API.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Role FindRoleByRoleId(int id)
        {
            return RoleDAO.FindRoleByRoleId(id);
        }

        public List<Role> GetRoles() => RoleDAO.GetRoles();
    }
}
