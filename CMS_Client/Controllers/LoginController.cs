﻿using CMS_Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace CMS_Client.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration configuration;
        private HttpClient client = null;
        private string apiurl = "";

        public LoginController(IConfiguration _configuration)
        {
            configuration = _configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            apiurl = configuration.GetValue<string>("Url")+ "api/Login";

        }
        public IActionResult Login()
        {
            String isLoggedIn = (String)HttpContext.Session.GetString("isLoggedIn");
            ViewBag["Name"] = (String)HttpContext.Session.GetString("UserCode");
            if (isLoggedIn != null && isLoggedIn.Equals("true"))
            {
                ViewBag["Name"]= HttpContext.Session.GetString("UserCode");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            if (ModelState.IsValid)
            {
                string strData = JsonConvert.SerializeObject(userLogin);
                HttpContent content = new StringContent(strData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiurl, content);
                if (response.IsSuccessStatusCode)
                {
                    // Get the token from response
                    var token = await response.Content.ReadAsStringAsync();

                    // Decode the token and get the role of account
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token.Replace('"', ' ').Trim());
                    var role = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                    var usercode = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

                    // Store data in session
                    HttpContext.Session.SetString("Role", role.ToString());
                    HttpContext.Session.SetString("UserCode", usercode);
                    HttpContext.Session.SetString("JWT", token.Replace('"', ' ').Trim());
                    HttpContext.Session.SetString("isLoggedIn", "true");
                    ViewBag["Name"] = usercode;
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Wrong username or password");
            }

            return View(userLogin);
        }

        // Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT");
            HttpContext.Session.Remove("JWT");
            HttpContext.Session.Remove("isLoggedIn");
            return RedirectToAction("Login", "Login");
        }
    }
}