using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;
using PRSshawhan.Models.DTOs;

namespace PRSshawhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly PrsDbContext _context;
        // Constructors
        public VendorsController(PrsDbContext context)
        {
            _context = context;
        }
        // Get vendor summary for one vendor
        // returns the vendor code, vendor name, and the count of products (VendorSummaryDTO)
        // GET: api/vendors/vendorsummary/{id}
        [HttpGet("vendorsummary/{id}")]
        public async Task<ActionResult<VendorSummaryDTO>> GetVendorSummary(int id)
        {
            try
            {
                var vendor = await _context.Vendors.Include(v => v.Products).Where(v => v.Id == id).Select(v => new VendorSummaryDTO(v.Code, v.Name, v.Products.Count)).FirstOrDefaultAsync();

                if (vendor == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return vendor;
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
        // Get all vendors
        // returns a list of vendors if successfull
        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            try
            {
                return await _context.Vendors.Include(v => v.Products).ToListAsync();
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
        // Get vendor by id
        // returns a single vendor if successfull
        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            try
            {
                var vendor = await _context.Vendors.Include(v => v.Products).FirstOrDefaultAsync(v => v.Id == id);

                if (vendor == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return vendor;
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
        // update vendor
        // returns a 204 NoContent code if successfull
        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
           
            if (id != vendor.Id)
            {
                // returns 400 - Bad Request
                return BadRequest();
            }
            try
            {
                _context.Entry(vendor).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                // returns a 204 No Content
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        // insert new vendor
        // returns the new vendors if successfull
        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            try
            {
                _context.Vendors.Add(vendor);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
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
        // delete vendor
        // returns a 204 NoContent code if successfull
        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            try
            {
                var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
                if (vendor == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                _context.Vendors.Remove(vendor);

                await _context.SaveChangesAsync();
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

        private bool VendorExists(int id)
        {
            return (_context.Vendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
