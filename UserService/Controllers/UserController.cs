using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/<BuildingController>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetBuilding()
        {
            return await _context.Users.ToListAsync();
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();
            var userCount = _context.Users.Count();
            User user = new User();
            user.Id=userObj.Id;
            user.UserId="UId"+(userCount+1).ToString();
            user.FirstName=userObj.FirstName;
            user.LastName=userObj.LastName;
            user.Email=userObj.Email;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Added Successfully"
            });

        }



    }
}
