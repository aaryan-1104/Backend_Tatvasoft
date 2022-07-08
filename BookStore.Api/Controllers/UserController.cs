using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        UserRepository _repository = new UserRepository();
        [HttpGet("getUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_repository.GetUsers());

        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            User user = _repository.Login(model);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            User user = _repository.Register(model);
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}
