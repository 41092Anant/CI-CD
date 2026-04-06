using CICD.Data;
using CICD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CICD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
        Ok(await _db.Products.ToListAsync<Product>());

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            this is not valid csharp code!!!
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Products.FindAsync(id);
            if (p == null) return NotFound();
            _db.Products.Remove(p);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
