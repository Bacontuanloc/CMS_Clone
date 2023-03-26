using CMS_Client.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMS_Client.Controllers
{
    public class AssignmentController : Controller
    {
        private IConfiguration configuration;
        private HttpClient client = null;
        private string apiurl = "";

        public AssignmentController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiurl = configuration.GetValue<string>("Url") + "api";

        }
        [HttpGet()]
        public async Task<IActionResult> Index(int id)
        {
            HttpResponseMessage response = await client.GetAsync(apiurl+ "/Assignment/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Assignment> listAss = JsonSerializer.Deserialize<List<Assignment>>(strData, options);

            HttpResponseMessage response2 = await client.GetAsync(apiurl + "/Material/GetMaterials?classid=" + id);
            string strData2 = await response2.Content.ReadAsStringAsync();

            List<Material> materials = JsonSerializer.Deserialize<List<Material>>(strData2, options);
            ViewData["classID"] = id;
            ViewBag.material = materials;
            return View(listAss);

        }

        [HttpGet()]
        public async Task<ActionResult> Edit(int classId, int assignmentId)
        {
            apiurl = $"https://localhost:7158/api/Assignment/classId/{classId}/assignmentId/{assignmentId}";
            HttpResponseMessage response = await client.GetAsync(apiurl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ViewData["classID"] = classId;
            Assignment assignment = JsonSerializer.Deserialize<Assignment>(strData, options);
            return View(assignment);
        }

        [HttpGet()]
        public async Task<IActionResult> Create(int id)
        {
            return View(id);
        }
        [HttpGet()]
        public async Task<IActionResult> CreateMaterial(int id)
        {
            return View(id);
        }
    }
}
