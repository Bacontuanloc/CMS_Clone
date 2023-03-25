using CMS_API.Repositories.Interfaces;
using CMS_API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CMS_API.Entities;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private IClassRepository repository = new ClassRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Class>> GetClasses()
        {
            return Ok(repository.GetClasses());
        }

        [HttpGet("{code}")]
        public ActionResult<IEnumerable<Class>> FindClassByClassCode(string code)
        {
            return Ok(repository.FindClassByClassCode(code));
        }

        [HttpPost]
        public IActionResult SaveClass(Class c)
        {
            repository.SaveClass(c);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateClass(Class c)
        {
            var cTmp = repository.FindClassById(c.ClassId);
            if (cTmp == null)
                return NotFound();
            repository.UpdateClass(c);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
