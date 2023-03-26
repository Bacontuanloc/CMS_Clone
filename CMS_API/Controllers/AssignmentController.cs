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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        [HttpGet("{code}")]
        public ActionResult<List<Assignment>> GetAssigmentByClassCode(string code)
        {
            var classcode = int.Parse(code);
            List<Assignment> list = new List<Assignment>();
            using (var context = new CMS_CloneContext())
            {
                list = context.Assignments.Where(c => c.ClassId == classcode).ToList();
            }
            return Ok(list);
        }

        [HttpGet("classId/{classId}/assignmentId/{assignmentId}")]
        public ActionResult<Assignment> GetAssigmentById(int assignmentId)
        {
            Assignment assignment = new Assignment();
            using (var context = new CMS_CloneContext())
            {
                assignment = context.Assignments.Where(a => a.AssignmentId == assignmentId).FirstOrDefault();
            }
            return Ok(assignment);
        }

        [HttpGet("Detail/{code}")]
        public ActionResult<Assignment> GetAssignmentDetail(string code)
        {
            var asscode = int.Parse(code);
            Assignment assignment = new Assignment();
            using (var context = new CMS_CloneContext())
            {
                assignment = context.Assignments.Where(c => c.AssignmentId == asscode).FirstOrDefault();
            }
            return Ok(assignment);
        }


        [HttpPost]
        [Authorize(Roles = "2")]
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
                ;
            }

            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateAssignment(AssignmentDTO c)
        {
            Assignment a = new Assignment();
            a.AssignmentId = c.AssignmentId;
            a.ClassId = c.ClassId;
            a.OwnerId = c.OwnerId;
            a.Title = c.Title;
            a.Description = c.Description;
            a.Deadline = c.Deadline;
            using (var context = new CMS_CloneContext())
            {
                a.Owner = context.Users.Where(u => u.UserId == c.OwnerId).FirstOrDefault();
                a.Class = context.Classes.Where(cl => cl.ClassId == c.ClassId).FirstOrDefault();
                var cTmp = context.Assignments.Where(ass => ass.AssignmentId == c.AssignmentId);
                if (cTmp == null)
                {
                    return NotFound();
                }
                context.Entry<Assignment>(a).State = EntityState.Modified;
                context.SaveChanges();
            }
            return NoContent();
        }


    }
}
