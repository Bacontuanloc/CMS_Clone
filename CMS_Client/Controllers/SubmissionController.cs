using CMS_Client.Entities;
using CMS_Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CMS_Client.Controllers
{
    public class SubmissionController : Controller
    {
        private IConfiguration configuration;
        private HttpClient client = null;
        private string apiurl = "";

        public SubmissionController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiurl = configuration.GetValue<string>("Url") + "api/";

        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            HttpResponseMessage response = await client.GetAsync(apiurl + "Assignment/Detail/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Assignment assignment  = JsonSerializer.Deserialize<Assignment>(strData, options);

            SubmitData submitData = new SubmitData
            {
                AssignmentId = assignment.AssignmentId,
                Description = assignment.Description,
                Title = assignment.Title,
                Submitted = true
            }; 

            return View(submitData);

        }
        [HttpGet]
        public async Task<IActionResult> Teacher(int id)
        {
            return View();

        }



    }
}
