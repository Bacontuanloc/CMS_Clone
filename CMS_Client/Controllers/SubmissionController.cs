using CMS_Client.Entities;
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
            apiurl = configuration.GetValue<string>("Url") + "api/Assignment";

        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            HttpResponseMessage response = await client.GetAsync(apiurl + "/Detail/" + id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Assignment assignment  = JsonSerializer.Deserialize<Assignment>(strData, options);
            return View(assignment);
        }



    }
}
