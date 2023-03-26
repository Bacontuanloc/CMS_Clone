using CMS_API.Repositories.Interfaces;
using CMS_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS_API.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository repository = new ClassRepository();
        private IUserClassRepository repositoryUserClass = new UserClassRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetClasses()
        {
            return Ok(repository.GetClasses());
        }

        [HttpGet("id/{id}")]
        public ActionResult<Class> FindClassById(int id)
        {
            return Ok(repository.FindClassById(id));
        }

        [HttpGet("code/{code}")]
        public ActionResult<Class> FindClassByClassCode(string code)
        {
            return Ok(repository.FindClassByClassCode(code));
        }

        [HttpGet("userId/{userId}")]
        public ActionResult<IEnumerable<Class>> FindClassByUserId(int userId)
        {
            return Ok(repository.FindClassByUserId(userId));
        }

        [HttpGet("userClassId/{userClassId}")]
        public ActionResult<IEnumerable<Class>> FindClassByUserClassId(int userClassId)
        {
            return Ok(repository.FindClassByUserClassId(userClassId));
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public IActionResult SaveClass(Class c)
        {
            repository.SaveClass(c);
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "1")]
        public IActionResult UpdateClass(Class c)
        {
            var cTmp = repository.FindClassById(c.ClassId);
            if (cTmp == null)
                return NotFound();
            repository.UpdateClass(c);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
        public IActionResult DeleteClass(int id)
        {
            var c = repository.FindClassById(id);
            if (c == null)
            {
                return NotFound();
            }
            repository.DeleteClass(c);
            return NoContent();
        }
    }
}
