using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceUser.Data;
using ServiceUser.Models;

namespace ServiceUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            
            // Don't return password hashes
            foreach (var user in users)
            {
                user.PasswordHash = null;
            }
            
            return users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            // Don't return password hash
            user.PasswordHash = null;
            return user;
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            user.RegisteredDate = DateTime.Now;
            // In a real app, hash the password here
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Don't return password hash
            user.PasswordHash = null;
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser([FromBody] LoginModel model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            // In a real app, verify the hashed password here
            if (user.PasswordHash != model.Password)
            {
                return Unauthorized();
            }

            // Don't return password hash
            user.PasswordHash = null;
            return user;
        }
    }
}