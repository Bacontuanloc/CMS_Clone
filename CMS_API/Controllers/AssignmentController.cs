using CMS_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        [HttpGet("{code}")]
        public ActionResult<List<Assignment>> GetAssigmentByClassCode(string code)
        {
            var classcode=int.Parse(code);
            List<Assignment> list = new List<Assignment>();
            using (var context = new CMS_CloneContext())
            {
                list = context.Assignments.Where(c=>c.ClassId==classcode).ToList();
            }
            return Ok(list);
        }



        [HttpGet("Detail/{code}")]
        public ActionResult<Assignment> GetAssignmentDetail(string code)
        {
            var asscode = int.Parse(code);
            Assignment assignment= new Assignment();
            using (var context = new CMS_CloneContext())
            {
                assignment = context.Assignments.Where(c => c.AssignmentId == asscode).FirstOrDefault();
            }
            return Ok(assignment);
        }

    }
}
