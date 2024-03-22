using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;
using PRSshawhan.Models.Utilities;

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
        // Rejects the request
        // Requires a rejection reason which is passed into the body
        // If successful, return the request
        // POST: api/requests/reject/{id}
        [HttpPost("reject/{id}")]
        public async Task<ActionResult<Request>> PostReject(int id, [FromBody]string reason)
        {
            try
            {
                var req = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
                if(req == null) { return NotFound("Request not found"); }
                //Check that the body includes a reason
                if(reason == null | reason == string.Empty) { return BadRequest(); }
                //Make sure the request status is Review
                if(req.Status.ToUpper() != PRSutilities.statusReview.ToUpper()) { return BadRequest(); }

                //Reject the request
                req.Status = PRSutilities.statusRejected;
                req.ReasonForRejection = reason;

                await _context.SaveChangesAsync();

                return req;
            }
            catch(SqlException sqlex)
            {
                //returns 500 Internal Server Error
                return Problem($"SQL Error: {sqlex.Message}");
            }
            catch(Exception ex)
            {
                //returns 500 Internal Server Error
                return Problem(ex.Message);
            }
        }
        //Approves the request
        //If successful, return the request
        //POST: api/requests/approve/id
        [HttpPost("approve/{id}")]
        public async Task<ActionResult<Request>> PostApprove(int id)
        {
            try
            {
                var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
                if(request == null) { return NotFound(); }
                //Make sure the request status is Review
                if (request.Status.ToUpper() != PRSutilities.statusReview.ToUpper()) { return BadRequest(); }
                //Approve the request
                request.Status = PRSutilities.statusApproved;
                await _context.SaveChangesAsync();
                return request;
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
        // Get all requests that are submitted for review and have not been placed by the current user
        // returns a list of requests if successfull
        // GET: api/Requests/reviews/{userid}
        [HttpGet("reviews/{userid}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userid)
        {
            try
            { 
                var requests = await _context.Requests.Where(r => r.UserId != userid && r.Status.ToUpper() == PRSutilities.statusReview.ToUpper()).Include(r => r.User).ToListAsync();
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userid);

                if(user == null || !user.Reviewer)
                {
                    // returns a 400 Bad Request
                    return BadRequest();
                }
                return requests;
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
        // Submit a request to review
        // returns a request if successfull
        // POST: api/requests/review/{id}
        [HttpPost("review/{id}")]
        public async Task<ActionResult<Request>> PostReviews(int id)
        {
            try
            {
                Request? request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

                if (request == null) { return NotFound(); }
                // Make sure the request status is New
                if (request.Status.ToUpper() != PRSutilities.statusNew.ToUpper()) { return BadRequest(); }

                //if the total of the request is $50 or less, approve the request otherwise set the request status to review
                if(request.Total <= 50) { request.Status = PRSutilities.statusApproved; } 
                else { request.Status = PRSutilities.statusReview; }

                await _context.SaveChangesAsync();

                return request;
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
        // Get all Requests
        // returns a list of requests if successfull
        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            try
            {
                return await _context.Requests.Include(r => r.User).Include(r => r.LineItems).ThenInclude(r => r.Product).ToListAsync();
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
        // Get request by ID
        // returns a single Request if successfull
        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            try
            {
                Request? request = await _context.Requests.Include(r => r.User).Include(r => r.LineItems).ThenInclude(r => r.Product).FirstOrDefaultAsync(r => r.Id == id);

                if (request == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return request;
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
        // Update Request
        // returns 204 NoContent code if successfull
        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                // returns a 400 Bad Request
                return BadRequest();
            }
            try
            {
                _context.Entry(request).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                //returns a 204 No Content
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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
        // Insert new Request
        // returns the newly created request if successfull
        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            try
            {
                _context.Requests.Add(request);
                await _context.SaveChangesAsync();
                // returns the newly created resource
                return CreatedAtAction("GetRequest", new { id = request.Id }, request);
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
        //Delete Request
        // returns 204 NoContent code if successfull
        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            try
            {
                Request? request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
                if (request == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
                //Returns a 204 No content
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

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
