using CodePulse.API.Database;
using CodePulse.API.DTOs.Category;
using CodePulse.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryListDto>>> GetCategories()
        {
            var categories = await _context.Categories.Select(x => new CategoryListDto
            {
                Id = x.Id,
                Name = x.Name,
                UrlHandle = x.UrlHandle

            }).ToListAsync();

            return categories;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryListDto>> GetCategory([FromRoute] Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            var categoryDto = new CategoryListDto
            {
                Id = id,
                Name = category.Name,
                UrlHandle = category.UrlHandle
            };

            return categoryDto;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute]Guid id, UpdateCategoryRequest dto)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                return BadRequest();
            }

            category.Name = dto.Name;
            category.UrlHandle = dto.UrlHandle;

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CreateCategoryRequestDto CreateCategoryRequestDto)
        {
            var category = new Category
            {
                Name = CreateCategoryRequestDto.Name,
                UrlHandle = CreateCategoryRequestDto.UrlHandle,
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
