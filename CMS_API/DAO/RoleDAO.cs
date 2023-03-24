using CMS_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS_API.DAO
{
    public class RoleDAO
    {
        public static List<Role> GetRoles()
        {
            var listRoles = new List<Role>();
            try
            {
                using (var context = new CMS_CloneContext())
                {
                    listRoles = context.Roles.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listRoles;
        }
    }
}
