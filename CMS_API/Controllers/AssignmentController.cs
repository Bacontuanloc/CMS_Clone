using CMS_API.Entities;
using CMS_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Metrics;

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


        [HttpPost]
        [Authorize(Roles ="2")]
        public IActionResult AddAssignment(AssignmentDTO c)
        {
            var x = new Assignment();
            x.Title = c.Title;
            x.Description = c.Description;
            x.Deadline = c.Deadline;
            x.ClassId = c.ClassId;
            x.OwnerId = c.OwnerId;
            using (var context = new CMS_CloneContext())
            {
                x.Owner = context.Users.Where(a => a.UserId == c.OwnerId).FirstOrDefault();
                x.Class = context.Classes.Where(a => a.ClassId == c.ClassId).FirstOrDefault();
                context.Assignments.Add(x);
                context.SaveChanges();
;            }
            
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateClass(Assignment c)
        {

            using (var context = new CMS_CloneContext())
            { 
                var cTmp = context.Assignments.Where(a=>a.AssignmentId==c.AssignmentId);
                if (cTmp == null) { 
                context.Entry<Assignment>(c).State =
                EntityState.Modified;
                context.SaveChanges();
                }
            }
            return NoContent();
        }


    }
}
