﻿using CMS_Client.Entities;
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

            var x = int.Parse(HttpContext.Session.GetString("userId"));

            HttpResponseMessage response1 = await client.GetAsync(apiurl + "Submission/GetAssignmentSubmited/?id=" + id+ "&userid="+x);
            string strData1 = await response1.Content.ReadAsStringAsync();

            Submission submission = JsonSerializer.Deserialize<Submission>(strData1, options);

            SubmitData submitData = new SubmitData
            {
                AssignmentId = assignment.AssignmentId,
                Description = assignment.Description,
                Title = assignment.Title,
                Submitted = true
            }; 
            if(submission== null) {
                submitData.Submitted= false;
            }

            return View(submitData);

        }
        [HttpGet]
        public async Task<IActionResult> Teacher(int id)
        {
            return View();

        }



    }
}
