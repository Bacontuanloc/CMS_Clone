using CMS_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaterialController : ControllerBase
    {

        [HttpGet]

        public async Task<IActionResult> GetMaterials(int classid)
        {
            List<Material> materials = new List<Material>();
            using (var context = new CMS_CloneContext())
            {
                materials = context.Materials.Where(c => c.ClassId == classid).ToList();
            }
            return Ok(materials);
        }


        [HttpPost]
        [Authorize(Roles = "2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] int classid, [FromForm] string title)
        {
            var result = await WriteFile(file, classid, title);
            return Ok(result);
        }

        private async Task<string> WriteFile(IFormFile file, int classid,string title)
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                CMS_CloneContext context = new CMS_CloneContext();
                Material materialused = context.Materials.Where(x => x.ClassId == classid).Where(c=>c.Title.Equals(title)).FirstOrDefault();

                filename = title + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Class" + classid);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                
                if (materialused != null)
                {
                    var path = Path.Combine(materialused.FilePath);
                    FileInfo file1 = new FileInfo(path);
                    if (file1.Exists)//check file exsit or not  
                    {
                        file1.Delete();
                    }
                }
                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Class" + classid,filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                if (materialused != null)
                {
                    materialused.FilePath = exactpath;
                    materialused.Title = title;
                    context.Materials.Update(materialused);
                    context.SaveChanges();
                }
                else
                {
                    Material material = new Material();
                    material.ClassId = classid;
                    material.Title = title;
                    material.FilePath = exactpath;
                    context.Materials.Add(material);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }
            return filename;
        }
    }
}
