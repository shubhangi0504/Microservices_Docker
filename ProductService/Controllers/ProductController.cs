using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;
        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: api/<BuildingController>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetBuilding()
        {
            return await _context.Products.ToListAsync();
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Product ProductObj)
        {
            if (ProductObj == null)
                return BadRequest();
            var prodCount = _context.Products.Count();
            Product product = new Product();
            product.Id = ProductObj.Id;
            product.ProductId="PId"+(prodCount+1).ToString();
            product.Name = ProductObj.Name;
            product.Price = ProductObj.Price;
            product.Quantity = ProductObj.Quantity;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Product Added Successfully"
            });

        }

    }
}
