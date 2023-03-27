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
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
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

            HttpResponseMessage response3 = await client.GetAsync(apiurl + "/UserClass/classId/" + id);
            string strData3 = await response3.Content.ReadAsStringAsync();
            List<UserClass> listUserClass = JsonSerializer.Deserialize<List<UserClass>>(strData3, options);
            List<int> listTeacher = new List<int>();
            foreach(UserClass uc in listUserClass){
                listTeacher.Add(uc.UserId);
            }
            ViewData["classID"] = id;
            ViewBag.material = materials;
            ViewBag.listTeacher = listTeacher;
            return View(listAss);

        }

        [HttpGet()]
        public async Task<ActionResult> Edit(int classId, int assignmentId)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
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
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(id);
        }
        [HttpGet()]
        public async Task<IActionResult> CreateMaterial(int id)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(id);
        }
    }
}
