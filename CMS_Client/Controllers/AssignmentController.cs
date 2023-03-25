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
            apiurl = configuration.GetValue<string>("Url") + "api/Assignment";

        }
        [HttpGet()]
        public async Task<IActionResult> Index(int id)
        {
            HttpResponseMessage response = await client.GetAsync(apiurl+"/"+id);
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Assignment> listAss = JsonSerializer.Deserialize<List<Assignment>>(strData, options);

            return View(listAss);
        }
    }
}
