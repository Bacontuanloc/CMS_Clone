using CMS_API.Entities;
using CMS_API.Models;
using CMS_API.Repositories;
using CMS_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
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
    }
}
