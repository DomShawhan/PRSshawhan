using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;

namespace PRSshawhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context)
        {
            _context = context;
        }
        // POST: api/requests/reject/{id}
        [HttpPost("reject/{id}")]
        public async Task<ActionResult<Request>> PostReject(int id, [FromBody]string reason)
        {
            //todo: only Reviewrs can Reject
            try
            {
                var req = await _context.Requests.FindAsync(id);
                if(req == null) { return NotFound("Request not found"); }

                if(reason == null | reason == string.Empty) { return BadRequest(); }
                if(req.Status.ToUpper() != PRSutilities.statusReview.ToUpper()) { return BadRequest(); }

                req.Status = PRSutilities.statusRejected;
                req.ReasonForRejection = reason;

                await _context.SaveChangesAsync();

                return req;
            }
            catch(SqlException sqlex)
            {
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        //POST: api/requests/approve/id
        [HttpPost("approve/{id}")]
        public async Task<ActionResult<Request>> PostApprove(int id)
        {
            try
            {
                var request = await _context.Requests.FindAsync(id);
                if(request == null) { return NotFound(); }
                if(request.Status.ToUpper() != PRSutilities.statusReview.ToUpper()) { return BadRequest(); }
                request.Status = PRSutilities.statusApproved;
                await _context.SaveChangesAsync();
                return request;
            }
            catch (SqlException sqlex)
            {
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //GET: api/Requests/reviews/{userid}
        [HttpGet("reviews/{userid}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userid)
        {
            try
            {
                var requests = await _context.Requests.Where(r => r.UserId != userid && r.Status.ToUpper() == PRSutilities.statusReview.ToUpper()).Include(r => r.User).ToListAsync();

                return requests;
            }
            catch (SqlException sqlex)
            {
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //POST: api/requests/review/{id}
        [HttpPost("review/{id}")]
        public async Task<ActionResult<Request>> PostReviews(int id)
        {
            try
            {
                var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

                if (request == null) { return NotFound(); }
                if (request.Status.ToUpper() != PRSutilities.statusNew.ToUpper()) { return BadRequest(); }

                if(request.Total <= 50) { request.Status = PRSutilities.statusApproved; } 
                else { request.Status = PRSutilities.statusReview; }

                await _context.SaveChangesAsync();

                return request;
            }
            catch (SqlException sqlex)
            {
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        { 
            return await _context.Requests.Include(r => r.User).Include(r => r.LineItems).ThenInclude(r => r.Product).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.Include(r => r.User).Include(r => r.LineItems).ThenInclude(r => r.Product).FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
          if (_context.Requests == null)
          {
              return Problem("Entity set 'PrsDbContext.Requests'  is null.");
          }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
