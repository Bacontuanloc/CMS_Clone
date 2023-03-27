using CMS_Client.Entities;
using CMS_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CMS_Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient client = null;
        private string ClassApiUrl = "";
        private string UserClassApiUrl = "";
        private string UserApiUrl = "";
        private string RoleApiUrl = "";

        public HomeController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ClassApiUrl = "https://localhost:7158/api/Class";
            UserClassApiUrl = "https://localhost:7158/api/UserClass";
            UserApiUrl = "https://localhost:7158/api/User";
            RoleApiUrl = "https://localhost:7158/api/Role";
        }

        public async Task<IActionResult> Index()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null )
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            HttpResponseMessage response = await client.GetAsync(UserClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<UserClass> listUserClass = JsonSerializer.Deserialize<List<UserClass>>(strData, options);
            //Get list of classid of userid
            var userId = int.Parse(HttpContext.Session.GetString("userId"));
            UserClassApiUrl = $"https://localhost:7158/api/UserClass/userId/{userId}";
            HttpResponseMessage responseUserClass = await client.GetAsync(UserClassApiUrl);
            string strDataUserClass = await responseUserClass.Content.ReadAsStringAsync();
            List<UserClass> list = JsonSerializer.Deserialize<List<UserClass>>(strDataUserClass, options);
            List<int> listClassIdOfStudent = new List<int>();
            foreach (UserClass userClass in list)
            {
                listClassIdOfStudent.Add(userClass.ClassId);
            }
            ViewBag.listClassIdOfStudent = listClassIdOfStudent;
            return View(listUserClass);
        }
        public async Task<IActionResult> Create()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            HttpResponseMessage response = await client.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<User> listTeacher = JsonSerializer.Deserialize<List<User>>(strData, options);
            return View(listTeacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class cl, IFormCollection collection)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            //Save Class
            cl.ClassId = 0;
            var payload = JsonSerializer.Serialize(cl);
            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(ClassApiUrl, c);
            string strData = await response.Content.ReadAsStringAsync();
            //Lay Class theo ClassCode
            ClassApiUrl = $"https://localhost:7158/api/Class/code/{cl.ClassCode}";
            HttpResponseMessage responseClass = await client.GetAsync(ClassApiUrl);
            string strDataClass = await responseClass.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Class cla = JsonSerializer.Deserialize<Class>(strDataClass, options);
            //Lay UserId tu Form
            var userId = Int32.Parse(collection["TeacherId"]);

            UserClassModel uc = new UserClassModel();
            uc.Id = 0;
            uc.UserId = userId;
            uc.ClassId = cla.ClassId;

            var payloadUserClass = JsonSerializer.Serialize(uc);
            HttpContent hc = new StringContent(payloadUserClass, Encoding.UTF8, "application/json");
            HttpResponseMessage responseUserClass = await client.PostAsync(UserClassApiUrl, hc);
            string strDataUserClass = await responseUserClass.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Edit(int id)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            //Get UserClass By ID
            UserClassApiUrl = $"https://localhost:7158/api/UserClass/id/{id}";
            HttpResponseMessage response = await client.GetAsync(UserClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            UserClass c = JsonSerializer.Deserialize<UserClass>(strData, options);

            //Get List Of Teachers
            HttpResponseMessage responseTeacher = await client.GetAsync(UserApiUrl);
            string strDataTeacher = await responseTeacher.Content.ReadAsStringAsync();
            var optionsTeacher = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<User> listTeacher = JsonSerializer.Deserialize<List<User>>(strDataTeacher, optionsTeacher);
            ViewBag.listTeacher = listTeacher;
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
            //Upate Class
            Class cl = new Class();
            cl.ClassId = Int32.Parse(collection["ClassId"]);
            cl.ClassCode = collection["ClassCode"];
            cl.Description = collection["Description"];
            var payload = JsonSerializer.Serialize(cl);
            HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(ClassApiUrl, c);
            string strData = await response.Content.ReadAsStringAsync();
            //Update UserClass
            UserClassModel uc = new UserClassModel();
            uc.Id = Int32.Parse(collection["UserClassId"]);
            uc.ClassId = Int32.Parse(collection["ClassId"]);
            uc.UserId = Int32.Parse(collection["TeacherId"]);
            var payloadUserClass = JsonSerializer.Serialize(uc);
            HttpContent hc = new StringContent(payloadUserClass, Encoding.UTF8, "application/json");
            HttpResponseMessage responseUserClass = await client.PutAsync(UserClassApiUrl, hc);
            string strDataUserClass = await responseUserClass.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            //Get Class By UserClass ID
            ClassApiUrl = $"https://localhost:7158/api/Class/userClassId/{id}";
            HttpResponseMessage response = await client.GetAsync(ClassApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Class c = JsonSerializer.Deserialize<Class>(strData, options);
            //Delete UserClass
            UserClassApiUrl = $"https://localhost:7158/api/UserClass/{id}";
            HttpResponseMessage responseUserClass = await client.DeleteAsync(UserClassApiUrl);
            string strDataUserClass = await responseUserClass.Content.ReadAsStringAsync();
            //Delete Class
            //ClassApiUrl = $"https://localhost:7158/api/Class/{c.ClassId}";
            //HttpResponseMessage responseClass = await client.DeleteAsync(ClassApiUrl);
            //string strDataClass = await responseClass.Content.ReadAsStringAsync();

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Enroll(int userId, int classId)
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            if (isLoggedIn == null)
            {
                return RedirectToAction("Error", "Home");
            }
            //Add UserClass
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
            UserClassModel uc = new UserClassModel();
            uc.Id = 0;
            uc.UserId = userId;
            uc.ClassId = classId;
            var payloadUserClass = JsonSerializer.Serialize(uc);
            HttpContent hc = new StringContent(payloadUserClass, Encoding.UTF8, "application/json");
            HttpResponseMessage responseUserClass = await client.PostAsync(UserClassApiUrl, hc);
            string strDataUserClass = await responseUserClass.Content.ReadAsStringAsync();
            //Get list ClassId of student

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Error()
        {

            //Get list ClassId of student

            return View("Error");
        }
    }
}