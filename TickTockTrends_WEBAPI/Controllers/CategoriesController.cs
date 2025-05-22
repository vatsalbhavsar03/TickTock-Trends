using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using TickTockTrends_WEBAPI.Data;
using TickTockTrends_WEBAPI.DTO;
using TickTockTrends_WEBAPI.Models;

namespace TickTockTrends_WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly TickTockDbContext _context;
        private readonly IConfiguration _configuration;

        public CategoriesController(TickTockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Categories
        [HttpGet("GetCategory")]
        public async Task<ActionResult> GetCategories()
        {
            var category = await _context.Categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,

            }).ToListAsync();
            return Ok(new
            {
                success = true,
                Message = "Categories fetched successfully",
                Category = category
            });
        }

        // GET: api/Categories/5
        [HttpGet("FindCategory/{Categoryid}")]
        public async Task<ActionResult<CategoryDTO>> FindCategory(int Categoryid)
        {
            var category = await _context.Categories.FindAsync(Categoryid);

            if (category == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Category not found"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Category fetched successfully",
                category = new CategoryDTO
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName
                }
            });
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateCategory/{Categoryid}")]
        public async Task<ActionResult> UpdateCategory(int Categoryid, [FromBody] CategoryDTO UpdateCategory)
        {
            if (_context.Categories.Any(c => c.CategoryName == UpdateCategory.CategoryName && c.CategoryId != Categoryid))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Category already exists"
                });
            }
            var category = await _context.Categories.FindAsync(Categoryid);
            if (category == null)
            {
                return NotFound($"Categories with Id {Categoryid} Not Found");

            }
            category.CategoryName = UpdateCategory.CategoryName;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Category updated successfully.",
                    category = new CategoryDTO
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    }

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Category: {ex.Message}");
            }
        }


        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddCategory")]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDTO AddCategory)
        {
            try
            {
                if (_context.Categories.Any(c => c.CategoryName == AddCategory.CategoryName))
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "This Category already exists."
                    });
                }

                var category = new Category
                {
                    CategoryName = AddCategory.CategoryName
                };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    success = true,
                    message = "Category Added Successfully",
                    Category = new
                    {
                        category.CategoryName,
                    },
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("DeleteCategory")]

        public async Task<IActionResult> DeleteCategory(int Categoryid)
        {
            var category = await _context.Categories.FindAsync(Categoryid);
            if (category == null)
            {
                return NotFound($"Category with Id {Categoryid} Not Found");
            }
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Category deleted successfully."
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error Deleting Category: {ex.Message}");
            }
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
