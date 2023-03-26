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
    public class UserClassController : ControllerBase
    {
        private IUserClassRepository repository = new UserClassRepository();
        private CMS_CloneContext context = new CMS_CloneContext();

        [HttpGet]
        public ActionResult<IEnumerable<UserClass>> GetClasses()
        {
            return Ok(repository.GetUserClass());
        }

        [HttpGet("id/{id}")]
        public ActionResult<UserClass> FindUserClassById(int id)
        {
            return Ok(repository.FindUserClassById(id));
        }

        [HttpGet("classId/{classId}")]
        public ActionResult<List<UserClass>> FindUserClassByClassId(int classId)
        {
            return Ok(repository.FindUserClassByClassId(classId));
        }

        [HttpGet("userId/{userId}")]
        public ActionResult<List<UserClass>> FindUserClassByUserId(int userId)
        {
            return Ok(repository.FindUserClassByUserId(userId));
        }

        [HttpPost]
        [Authorize(Roles = "1,3")]
        public IActionResult SaveUserClass(UserClassModel c)
        {
            repository.SaveUserClass(c);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public IActionResult UpdateUserClass(UserClassModel c)
        {
            var cTmp = repository.FindUserClassById(c.Id);
            if (cTmp == null)
                return NotFound();
            repository.UpdateUserClass(c);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteUserClass(int id)
        {
            var d = repository.FindUserClassById(id);
            if (d == null)
            {
                return NotFound();
            }
            repository.DeleteUserClass(d);
            return NoContent();
        }
    }
}
