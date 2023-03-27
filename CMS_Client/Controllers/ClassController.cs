using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using CMS_Client.Entities;

namespace CMS_Client.Controllers
{
    public class ClassController : Controller
    {
        private readonly HttpClient client = null;
        private string ClassApiUrl = "";

        public ClassController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ClassApiUrl = "https://localhost:7158/api/Class";
        }

        public async Task<IActionResult> Index()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            var userId = int.Parse(HttpContext.Session.GetString("userId"));
            ClassApiUrl = $"https://localhost:7158/api/Class/userId/{userId}";
            HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Class> listClasses = JsonSerializer.Deserialize<List<Class>>(strData, options);

            ViewBag.userId = userId;

            return View(listClasses);
        }

        public async Task<IActionResult> Create()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class cl)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            cl.ClassId = 0;
            var payload = JsonSerializer.Serialize(cl);

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ClassApiUrl, c);
            string strData = await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Class");
        }

        public async Task<ActionResult> Edit(int id)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            ClassApiUrl = $"https://localhost:7158/api/Class/?id={id}";
            HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Class c = JsonSerializer.Deserialize<Class>(strData, options);
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection collection)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            Class cl = new Class();
            cl.ClassId = Int32.Parse(collection["ClassId"]);
            cl.ClassCode = collection["ClassCode"];
            cl.Description = collection["Description"];

            var payload = JsonSerializer.Serialize(cl);

            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(ClassApiUrl, c);
            string strData = await response.Content.ReadAsStringAsync();

            ClassApiUrl = $"https://localhost:7158/api/Class/{cl.ClassId}";
            response = await client.GetAsync(ClassApiUrl);
            strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Class cla = JsonSerializer.Deserialize<Class>(strData, options);
            //return View(cla);
            return RedirectToAction("Index", "Class");
        }

        //public async Task<IActionResult> Details(int id)
        //{
        //    ClassApiUrl = $"https://localhost:7158/api/Class/{id}";
        //    HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
        //    string strData = await response.Content.ReadAsStringAsync();

        //    var options = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
        //    Class c = JsonSerializer.Deserialize<Class>(strData, options);
        //    return View(c);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            ClassApiUrl = $"https://localhost:7158/api/Class/{id}";
            HttpResponseMessage response = await client.DeleteAsync(ClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Index", "Class");
        }
    }
}
