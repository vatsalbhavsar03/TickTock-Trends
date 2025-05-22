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

        // GET: api/Products/5
        [HttpGet("GetProductById/{Productid}")]
        public IActionResult GetProductById(int Productid)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.ProductId == Productid);

            if (product == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Product not found"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Product fetched successfully",
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

        // GET: api/Products/GetProductsByCategoryOrBrand/5
        [HttpGet("GetProductsByCategoryOrBrand")]
        public async Task<IActionResult> GetProductsByCategoryOrBrand([FromQuery] int? categoryId, [FromQuery] int? brandId)
        {
            var query = _context.Products.AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (brandId.HasValue)
                query = query.Where(p => p.BrandId == brandId.Value);

            var products = await query
                .Select(p => new
                {
                    p.ProductId,
                    p.Name,
                    p.Price,
                    p.Stock,
                    p.ImageUrl,
                    p.Description,
                    p.CategoryId,
                    p.BrandId,
                   
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Products fetched successfully.",
                products = products
            });
        }


        // PUT: api/Products/5
        [HttpPut("UpdateProduct/{Productid}")]
        public async Task<IActionResult> UpdateProduct(int Productid, [FromForm] Productdto dto)
        {
            var existingProduct = await _context.Products.FindAsync(Productid);
            if (existingProduct == null)
            {
                return NotFound(new 
                {
                    success = false,
                    message = "Product not found"
                });
            }


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
            return Ok(new 
            {
                success = true,
                message = "Product updated successfully",
                product = new
                {
                    existingProduct.ProductId,
                    existingProduct.Name,
                    existingProduct.Price,
                    existingProduct.Stock,
                    existingProduct.Description,
                    existingProduct.ImageUrl,
                    existingProduct.CategoryId,
                    existingProduct.BrandId
                }
            });
        }


        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromForm] Productdto productDto)
        {
            try
            {
                // Check if image is uploaded
                if (productDto.ImageUrl == null || productDto.ImageUrl.Length == 0)
                    return BadRequest(new 
                    { 
                        success = false, 
                        message = "Image file is required." 
                    });

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

               
                return Ok(new 
                { 
                    success = true, 
                    message = "Product added successfully.",
                    product = new
                    {
                        product.ProductId,
                        product.Name,
                        product.Price,
                        product.Stock,
                        product.Description,
                        product.ImageUrl,
                        product.CategoryId,
                        product.BrandId
                    }
                });
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        // DELETE: api/Products/5
        [HttpDelete("DeleteProduct/{Productid}")]
        public IActionResult DeleteProduct(int Productid)
        {
            var product = _context.Products.Find(Productid);
            if (product == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Product not found"
                });
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return Ok(new 
            { 
                success = true, 
                message = "Product deleted successfully" 
            });
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
