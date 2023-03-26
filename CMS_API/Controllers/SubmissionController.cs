using CMS_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Reflection.Emit;
using System.IO;
using System.Drawing;

namespace CMS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<Submission> GetAssignmentSubmited(int id,int userid)
        {
            CMS_CloneContext context = new CMS_CloneContext();
            return Ok(context.Submissions.Where(c => c.AssignmentId == id&&c.OwnerId==userid).FirstOrDefault());
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file,[FromForm] int assignmentid,[FromForm] int userid)
        {
            var result = await WriteFile(file, assignmentid, userid);
            return Ok(result);
        }

        private async Task<string> WriteFile(IFormFile file, int assignmentid, int userid)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                CMS_CloneContext context= new CMS_CloneContext();
                Submission submissionsubmited= context.Submissions.Where(sub=>sub.OwnerId == userid).Where(x=>x.AssignmentId==assignmentid).FirstOrDefault();
               
                var user= context.Users.FirstOrDefault(x => x.UserId==userid);
                var date = DateTime.Now;
                filename = user.UserCode+"_"+ date.Ticks.ToString() + extension;


                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\Assignment"+assignmentid);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                
                if (submissionsubmited != null)
                {
                    var path = Path.Combine(submissionsubmited.FilePath);
                    FileInfo file1 = new FileInfo(path);
                    if (file1.Exists)//check file exsit or not  
                    {
                        file1.Delete();
                    }
                }
                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\Assignment" + assignmentid, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    
                }
                if(submissionsubmited != null)
                {
                    submissionsubmited.SubmissionTime = date;
                    submissionsubmited.FilePath = exactpath;
                    context.Submissions.Update(submissionsubmited);
                    context.SaveChanges();
                }
                else
                {
                    Submission submission = new Submission();
                    submission.AssignmentId = assignmentid;
                    submission.OwnerId = userid;
                    submission.SubmissionTime = date;
                    submission.FilePath = exactpath;
                    context.Submissions.Add(submission);
                    context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
            }
            return filename;
        }



        [HttpGet]

        public async Task<IActionResult> DownloadFile(string filename)
        {
            var filepath = Path.Combine(filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }



        [HttpGet]

        public async Task<IActionResult> GetSubmission(int assignmentid)
        {
            List<Submission> submissions = new List<Submission>();
            using (var context = new CMS_CloneContext())
            {
                submissions = context.Submissions.Where(c => c.AssignmentId == assignmentid).ToList();
            }
            return Ok(submissions);
        }

        [HttpPost("{submissionid}")]

        public async Task<IActionResult> Grade(int submissionid, int grade, string feedback)
        {
            using (var context = new CMS_CloneContext())
            {
                var submit = context.Submissions.Where(c => c.SubmissionId == submissionid).FirstOrDefault();
                submit.Grade=grade;
                submit.Feedback=feedback;
                context.Submissions.Update(submit);
                context.SaveChanges();
            }
            return Ok();
        }

    }
}