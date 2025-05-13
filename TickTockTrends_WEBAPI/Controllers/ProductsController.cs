using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Data;
using TickTockTrends_WEBAPI.DTO;
using TickTockTrends_WEBAPI.Models;

namespace TickTockTrends_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Products
        [HttpGet("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)  
                .Include(p => p.Brand)    
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.Description,
                    p.ImageUrl,
                    CategoryName = p.Category.CategoryName,  
                    BrandName = p.Brand.BrandName            
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Products fetched successfully",
                products = products
            });
        }





        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                product.ProductId,
                product.Name,
                product.Price,
                product.Stock,
                product.Description,
                product.ImageUrl,
                product.CategoryId,
                product.BrandId,
                CategoryName = product.Category?.CategoryName,
                BrandName = product.Brand?.BrandName
            });
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] Productdto dto)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = dto.Name;
            existingProduct.Price = dto.Price;
            existingProduct.Stock = dto.Stock;
            existingProduct.Description = dto.Description;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            if (dto.ImageUrl != null && dto.ImageUrl.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.ImageUrl.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageUrl.CopyToAsync(stream);
                }

                existingProduct.ImageUrl = $"/uploads/{fileName}";
            }

            existingProduct.CategoryId = dto.CategoryId;
            existingProduct.BrandId = dto.BrandId;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Product updated successfully" });
        }





        //[HttpPut("UpdateProduct/{id}")]
        //public IActionResult UpdateProduct(int id, [FromBody] Productdto productDto)
        //{
        //    var existingProduct = _context.Products.Include(p => p.Category).Include(p => p.Brand).FirstOrDefault(p => p.ProductId == id);
        //    if (existingProduct == null)
        //    {
        //        return NotFound();
        //    }

        //    // Update the product 
        //    existingProduct.Name = productDto.Name;
        //    existingProduct.Price = productDto.Price;
        //    existingProduct.Stock = productDto.Stock;
        //    existingProduct.Description = productDto.Description;

        //    // If the image is updated, handle it
        //    if (productDto.ImageUrl != null)
        //    {
        //        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrl.FileName);
        //        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

        //        Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);
        //        using (var stream = new FileStream(imagePath, FileMode.Create))
        //        {
        //            productDto.ImageUrl.CopyTo(stream);
        //        }
        //        existingProduct.ImageUrl = $"/uploads/{fileName}"; 
        //    }

        //    // Update Category and Brand 
        //    var category = _context.Categories.FirstOrDefault(c => c.CategoryId == productDto.CategoryId);
        //    var brand = _context.Brands.FirstOrDefault(b => b.BrandId == productDto.BrandId);

        //    if (category != null && brand != null)
        //    {
        //        existingProduct.CategoryId = category.CategoryId;
        //        existingProduct.BrandId = brand.BrandId;
        //        existingProduct.Category = category;
        //        existingProduct.Brand = brand;
        //    }
        //    else
        //    {
        //        return BadRequest("Invalid category or brand.");
        //    }

        //    _context.SaveChanges();

        //    return Ok(new { success = true, message = "Product updated successfully" });
        //}



        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] Productdto productDto)
        {
            try
            {
                // Check if image is uploaded
                if (productDto.ImageUrl == null || productDto.ImageUrl.Length == 0)
                    return BadRequest(new { success = false, message = "Image file is required." });

                //  uploaded image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(productDto.ImageUrl.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(imagePath)!);

                // Save the image to the server
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await productDto.ImageUrl.CopyToAsync(stream);
                }

               
                var product = new Product
                {
                    Name = productDto.Name,
                    CategoryId = productDto.CategoryId,
                    BrandId = productDto.BrandId,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    Description = productDto.Description,
                    ImageUrl = $"/uploads/{fileName}",  
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

               
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

               
                return Ok(new { success = true, message = "Product added successfully." });
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { success = false, message = $"Error: {ex.Message}" });
            }
        }






        // DELETE: api/Products/5
        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(new { success = true, message = "Product deleted successfully" });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
