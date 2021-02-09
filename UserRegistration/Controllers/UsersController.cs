using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Models;
using System.Text.Json;
using System.IO;
using System.Reflection;

namespace UserRegistration.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private static List<User> users = new List<User>();
        private static int count = 0;
        string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        static UsersController()
        {
            User user1 = new User
            {
                Id = count++,
                FirstName = "Mohan",
                LastName = "Kandavel",
                DOB = "28/03/1996",
                Email = "mohan@gmail.com"
            };
            User user2 = new User
            {
                Id = count++,
                FirstName = "Robert",
                LastName = "Lee",
                DOB = "04/03/1997",
                Email = "robert@gmail.com"
            };
            users.Add(user1);
            users.Add(user2);
        }


        // GET api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return users;
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(users.Where(user => user.Id == id).FirstOrDefault());
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            user.Id = count++;
            users.Add(user);
            String json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(path + "output.json", json);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            User found = users.Where(n => n.Id == id).FirstOrDefault();
            found.FirstName = user.FirstName;
            found.LastName = user.LastName;
            found.DOB = user.DOB;
            found.Email = user.Email;



            return NoContent();
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            users.RemoveAll(n => n.Id == id);

            return NoContent();
        }
    
    }
}
