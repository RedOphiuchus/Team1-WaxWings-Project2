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
        [HttpGet("{username}")]
        public ActionResult<bool> ValidateUsername(string username)
        {
            return _UserRepository.validateusername(username);
        }

        // GET boolean for login
        [HttpGet("{username}/{password}")]
        public ActionResult<bool> ValidateLogin(string username,string password)
        {
            return _UserRepository.validatelogin(username,password);
        }

        // GET boolean for register
        [HttpPost("Register/{username}/{password}")]
        public ActionResult<bool> Register(string username, string password)
        {
            bool validated = _UserRepository.validateusername(username);
            bool success = false;
            if (validated == false)
            {
                Domain.User user1 = new Domain.User(username,password);

                _UserRepository.AddUser(user1);
                _UserRepository.Save();
                if (_UserRepository.validateusername(username) == true)
                {
                    success = true;
                }
            }

            return success;
        }
    }
}
