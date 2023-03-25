﻿using CMS_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CMS_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        [HttpPost]
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
                var user= context.Users.FirstOrDefault(x => x.UserId==userid);
                var date = DateTime.Now;
                filename = user.UserCode+"_"+ date.Ticks.ToString() + extension;


                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\"+assignmentid);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files\\" + assignmentid, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                Submission submission= new Submission();
                submission.AssignmentId= assignmentid;
                submission.OwnerId= userid;
                submission.SubmissionTime = date;
                submission.FilePath=exactpath;
                context.Submissions.Add(submission);
            }
            catch (Exception ex)
            {
            }
            return filename;
        }



        [HttpGet]
        public async Task<IActionResult> DownloadFile(string filename, int assignmentid, int userid)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }

    }
}