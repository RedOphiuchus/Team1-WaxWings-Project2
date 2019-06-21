using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //intialize repository objects so that we can use the repository methods
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        // GET boolean for validating username
        //[HttpGet("{username}")]
        //public ActionResult<bool> ValidateUsername(string username)
        //{
        //    return _UserRepository.validateusername(username);
        //}

        // POST boolean for login
        [HttpPost("Login")]
        //public IActionResult Login([FromBody] Domain.User user)
        public IActionResult Login([FromBody] Domain.User user)
        {
            bool success = _UserRepository.validatelogin(user.username,user.password);
            if(success)
            {
                Domain.User realUser = _UserRepository.GetUserByUsername(user.username);
                return Ok(new { user = realUser.id });
            }
            return Unauthorized(new { message = "Username doesn't exist or password is incorrect." });
        }

        // GET boolean for register
        [HttpPost("Register/")]
        public IActionResult Register([FromBody] Domain.User user)
        {
            bool validated = _UserRepository.validateusername(user.username);
            if (validated == true)
                return BadRequest(new { message = "Username already exists." });

            _UserRepository.AddUser(user);
            _UserRepository.Save();
            if (_UserRepository.validateusername(user.username) == false)
                return StatusCode(500);

            return Created("api/User/Register", _UserRepository.GetUserByUsername(user.username).id);
        }
    }
}
