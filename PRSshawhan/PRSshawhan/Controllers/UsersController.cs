using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;
using PRSshawhan.Models.DTOs;
using PRSshawhan.Models.EF;

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

        //todo: Post: /api/users/login  pass as body username and password
        // POST: /api/users/login
        [HttpPost("login")]
        public async Task<ActionResult> PostLogin(LoginDTO loginInfo)
        {
            var user = await _context.Users.Where(u => u.Username == loginInfo.Username && u.Password == loginInfo.Password).FirstOrDefaultAsync();

            if(user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //todo: add try/catch
            return await _context.Users.Include(u => u.Requests).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
              //todo: add try/catch
              var user = await _context.Users.Include(u => u.Requests).FirstOrDefaultAsync(u => u.Id == id);

              if (user == null)
              {
                  return NotFound();
              }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'PrsDbContext.Users'  is null.");
          }
            _context.Users.Add(user);
            //todo: add try catch
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            //todo: add try catch
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
