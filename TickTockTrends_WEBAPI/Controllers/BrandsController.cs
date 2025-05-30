﻿using System;
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
    public class BrandsController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public BrandsController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Brands
        [HttpGet("GetBrands")]
        public async Task<ActionResult> GetBrands()
        {
            var brands = await _context.Brands
                .Include(b => b.Category)
                .Select(b => new 
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.CategoryName
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Brand fetched successfully",
                brand = brands
            });
        }

     

        // GET: api/Brands/5
        [HttpGet("FindBrand/{Brandid}")]
            public async Task<ActionResult<BrandDTO>> FindBrand(int Brandid)
            {
                var brand = await _context.Brands.FindAsync(Brandid);

                if (brand == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Brand not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Brand fetched successfully",
                    brand = new BrandDTO
                    {
                        BrandId = brand.BrandId,
                        BrandName = brand.BrandName,
                        CategoryId = brand.CategoryId
                    }
                });
            }

        [HttpGet("GetBrandsByCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetBrandsByCategory(int categoryId)
        {
            var brands = await _context.Brands
                .Where(b => b.CategoryId == categoryId)
                .Select(b => new BrandDTO
                {
                    BrandId = b.BrandId,
                    BrandName = b.BrandName,
                    CategoryId = b.CategoryId
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                message = "Brands fetched successfully",
                brands = brands
            });
        }



        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateBrand/{Brandid}")]
        public async Task<ActionResult> UpdateBrand(int Brandid, [FromBody] BrandDTO UpdateBrand)
        {
            if (_context.Brands.Any(b => b.BrandName == UpdateBrand.BrandName && b.BrandId != Brandid))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Brand already exists"
                });
            }
            var brand = await _context.Brands.FindAsync(Brandid);
            if (brand == null)
            {
                return NotFound($"Brand with Id {Brandid} Not Found");

            }

            brand.CategoryId = UpdateBrand.CategoryId;
            brand.BrandName = UpdateBrand.BrandName;


            try
            {
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Brand updated successfully.",
                    Brand = new
                    {
                        brand.BrandName,
                        brand.CategoryId,
                    },
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Brand: {ex.Message}");
            }
        }

        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddBrand")]
        public async Task<ActionResult> AddBrand([FromBody] BrandDTO AddBrand)
        {
            try
            {
                if (_context.Brands.Any(b => b.BrandName == AddBrand.BrandName))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "This Brand already exists."
                    });
                }

                var brand = new Brand
                {
                    CategoryId=AddBrand.CategoryId,
                    BrandName=AddBrand.BrandName
                    
                };
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Brand Added Successfully",
                    Brand = new
                    {
                        brand.BrandName,
                        brand.CategoryId,
                    },
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/Brands/5
        [HttpDelete("DeleteBrand")]

        public async Task<IActionResult> DeleteBrand(int Brandid)
        {
            var brand = await _context.Brands.FindAsync(Brandid);
            if (brand == null)
            {
                return NotFound($"Brand with Id {Brandid} Not Found");
            }
            try
            {
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Brand deleted successfully."
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Deleting Brand: {ex.Message}");
            }
        }

        private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.BrandId == id);
        }
    }
}
