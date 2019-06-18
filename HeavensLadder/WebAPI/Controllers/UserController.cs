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

        // GET boolean for username
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

        // GET boolean for login
        [HttpPost("Register/{username}/{password}")]
        public ActionResult<bool> Register(string username, string password)
        {
            bool validated = _UserRepository.validateusername(username);
            if (validated == false)
            {
                Domain.User user1 = new Domain.User(username,password);

                _UserRepository.AddUser(user1);
                _UserRepository.Save();
            }

            return _UserRepository.validateusername(username);
        }

        /*
        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
