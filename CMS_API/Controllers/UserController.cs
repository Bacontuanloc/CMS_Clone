using CMS_API.Entities;
using CMS_API.Models;
using CMS_API.Repositories;
using CMS_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMS_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository repository = new UserRepository();

        [HttpGet("Teacher")]
        [Authorize(Roles = "2")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Username}, you are an {currentUser.RoleId}");
        }


        [HttpGet("Admin")]
        [Authorize(Roles = "1")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Username}, you are a {currentUser.RoleId}");
        }

        [HttpGet("Student")]
        [Authorize(Roles = "3")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Username}, you are an {currentUser.RoleId}");
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    RoleId =int.Parse(userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllTeacher()
        {
            return Ok(repository.GetAllTeacher());
        }

        [HttpGet("userId/{userId}")]
        public ActionResult<User> FindUserByUserId(int userId)
        {
            return Ok(repository.FindUserByUserId(userId));
        }
    }
}

