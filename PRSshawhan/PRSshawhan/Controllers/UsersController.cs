using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;
using PRSshawhan.Models.DTOs;
using PRSshawhan.Models.Utilities;

namespace PRSshawhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // converts to and from JSON on API calls
    public class UsersController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public UsersController(PrsDbContext context)
        {
            _context = context;
        }
        // Get user summary by id
        // returns the UserSummaryDTO
        // GET: api/Users/usersummary/5
        [HttpGet("usersummary/{id}")]
        public async Task<ActionResult<UserSummaryDTO>> GetUserSummary(int id)
        {
            try
            {
                // Get count of user requests
                int countOfRejected = await _context.Requests
                    .Where(r => r.UserId == id && r.Status == PRSutilities.statusRejected).CountAsync();
                int countOfAccepted = await _context.Requests
                    .Where(r => r.UserId == id && r.Status == PRSutilities.statusApproved).CountAsync();
                int countOfPending = await _context.Requests
                    .Where(r => r.UserId == id && r.Status == PRSutilities.statusReview).CountAsync();
                // Get total of user requests
                decimal approvedTotal = await _context.Requests
                    .Where(r => r.UserId == id && r.Status == PRSutilities.statusApproved).SumAsync(r => r.Total);
                decimal rejectedTotal = await _context.Requests
                    .Where(r => r.UserId == id && r.Status == PRSutilities.statusRejected).SumAsync(r => r.Total);

                var user = await _context.Users.Include(u => u.Requests).Where(u => u.Id == id)
                    .Select(u => new UserSummaryDTO(
                        u.Firstname, u.Lastname, countOfRejected, countOfAccepted, countOfPending, approvedTotal, rejectedTotal)
                    ).FirstOrDefaultAsync();

                if (user == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return user;
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // Login a user
        // Returns the user if successfull
        // POST: /api/users/login
        [HttpPost("login")]
        public async Task<ActionResult> PostLogin(LoginDTO loginInfo)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Username == loginInfo.Username && u.Password == loginInfo.Password).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest();
                }
                // returns 200 Ok with the user
                return Ok(user);
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // Get all users
        // returns a list of users if successfull
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return await _context.Users.Include(u => u.Requests).ToListAsync();
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // Get user by id
        // returns a single user if successfull
        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Requests).FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return user;
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // Update a user
        // returns a 204 NoContent code if successfull
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                // Returns a 400 Bad Request
                return BadRequest();
            }
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                // returns a 204 No Content
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    // returns 404 Not Found
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // Insert a new user
        // returns the created user if successfull
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            int id = -1;
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                id = user.Id;
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        // delete a user
        // returns a 204 NoContent code if successfull
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                // Returns a 204 No Content
                return NoContent();
            }
            catch (SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
