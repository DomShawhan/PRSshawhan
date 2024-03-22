using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;
using PRSshawhan.Models.EF;

namespace PRSshawhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public LineItemsController(PrsDbContext context)
        {
            _context = context;
        }
        // Get line items for a request
        // Returns a list of line items if successfull
        // GET: api/lines-for-pr/{reqid}
        [HttpGet("/api/lines-for-pr/{reqid}")]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItemsForPr(int reqid)
        {
            try
            {
                return await _context.LineItems.Include(li => li.Product).Where(li => li.RequestId == reqid).ToListAsync();
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
        // Get all line items
        // returns a list of line items if successfull
        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItems()
        {
            try
            {
                return await _context.LineItems.Include(li => li.Product).Include(li => li.Request).ToListAsync();
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
        //Get line item by Id
        // returns a single line item if successfull
        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItem>> GetLineItem(int id)
        {
            try
            {
                var lineItem = await _context.LineItems.Include(li => li.Product).Include(li => li.Request).FirstOrDefaultAsync(li => li.Id == id);

                if (lineItem == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return lineItem;
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
        // Update line item
        // returns a 204 NoContent code if successfull
        // PUT: api/LineItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineItem(int id, LineItem lineItem)
        {
            try
            {
                if (id != lineItem.Id)
                {
                    // Returns a 400 Bad Request
                    return BadRequest();
                }

                _context.Entry(lineItem).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                await RecalculateRequestTotal(lineItem.RequestId);

                // Returns a 204 No Content
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
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
        // Insert new line item
        // returns the new line item if successfull
        // POST: api/LineItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
        {
            try
            {
                _context.LineItems.Add(lineItem);
                await _context.SaveChangesAsync();
                await RecalculateRequestTotal(lineItem.RequestId);

                return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
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
        // Delete line item
        // returns a 204 NoContent code if successfull
        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineItem(int id)
        {
            try
            {
                var lineItem = await _context.LineItems.FirstOrDefaultAsync(li => li.Id == id);
                if (lineItem == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }
                int requestId = lineItem.RequestId;
                _context.LineItems.Remove(lineItem);

                await _context.SaveChangesAsync();
                await RecalculateRequestTotal(requestId);
                // returns a 204 No Content
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

        private bool LineItemExists(int id)
        {
            return (_context.LineItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // Update the request total
        private async Task RecalculateRequestTotal(int requestid)
        {
            //Find request
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestid);
            // Calculate total of the request
            decimal total = await _context.LineItems.Where(li => li.RequestId == requestid).Include(li => li.Product).SumAsync(li => li.Quantity * li.Product.Price);
            
            request.Total = total;
            await _context.SaveChangesAsync();
        }
    }
}
