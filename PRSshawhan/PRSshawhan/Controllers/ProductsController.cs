using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSshawhan.Models;

namespace PRSshawhan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public ProductsController(PrsDbContext context)
        {
            _context = context;
        }
        // Get a all products
        // returns a list of product if successfull
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return await _context.Products.Include(p => p.Vendor).ToListAsync();
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
        // Get Product by ID
        // returns a single product if successfull
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _context.Products.Include(p => p.Vendor).FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                return product;
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
        // Update a product
        // returns a 204 NoContent code if successfull
        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                // Returns a 400 Badd Request
                return BadRequest();
            }

            try
            {
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                // returns a 204 No Content
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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
        // Insert a new product
        // returns the new project if successfull
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
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
        // Delete a product
        // returns a 204 NoContent code if successfull
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product == null)
                {
                    // returns 404 Not Found
                    return NotFound();
                }

                _context.Products.Remove(product);
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

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
