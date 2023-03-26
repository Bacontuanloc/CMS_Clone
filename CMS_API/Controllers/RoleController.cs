using CMS_API.Entities;
using CMS_API.Models;
using CMS_API.Repositories;
using CMS_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleRepository repository = new RoleRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            return repository.GetRoles();
        }

        [HttpGet("roleId/{roleId}")]
        public ActionResult<User> FindUserByUserId(int roleId)
        {
            return Ok(repository.FindRoleByRoleId(roleId));
        }
    }
}
