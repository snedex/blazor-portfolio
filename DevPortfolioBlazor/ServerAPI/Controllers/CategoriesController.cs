using Core.Models;
using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private AppDbContext _appDbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            var categories = await _appDbContext.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet("withposts")]
        public async Task<IActionResult> GetWithPosts()
        {
            var categories = await _appDbContext.Categories
                .Include(category => category.Posts)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await GetCategoryByCategoryId(id, false);

            return Ok(category);
        }

        [HttpGet("withposts/{id}")]
        public async Task<IActionResult> GetWithPosts(int id)
        {
            var category = await GetCategoryByCategoryId(id, true);

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category categoryToCreate)
        {
            try
            {
                if (categoryToCreate == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                await _appDbContext.Categories.AddAsync(categoryToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, Helpers.c_HTTP500Message_Short);
                }
                else
                {
                    return Created("Create", categoryToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Helpers.c_HTTP500Message_Long {e.Message}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryViewModel updatedCategory)
        {
            try
            {
                if (id < 1 || updatedCategory == null || id != updatedCategory.CategoryId)
                {
                    return BadRequest(ModelState);
                }

                var dbCategory = await _appDbContext.Categories.FirstOrDefaultAsync(category => category.CategoryId == id);

                if (dbCategory == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                dbCategory.Name = updatedCategory.Name;
                dbCategory.Description = updatedCategory.Description;   
                dbCategory.ThumbnailPath = updatedCategory.ThumbnailPath;   

                _appDbContext.Categories.Update(dbCategory);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, Helpers.c_HTTP500Message_Short);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{Helpers.c_HTTP500Message_Long} {e.Message}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest(ModelState);
                }

                bool exists = await _appDbContext.Categories.AnyAsync(category => category.CategoryId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Category categoryToDelete = await GetCategoryByCategoryId(id, false);

                if (categoryToDelete.ThumbnailPath != "uploads/placeholder.jpg")
                {
                    string fileName = categoryToDelete.ThumbnailPath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _appDbContext.Categories.Remove(categoryToDelete);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, Helpers.c_HTTP500Message_Short);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Helpers.c_HTTP500Message_Long {e.Message}.");
            }
        }

        #region Utility methods

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<bool> PersistChangesToDatabase()
        {
            int amountOfChanges = await _appDbContext.SaveChangesAsync();

            return amountOfChanges > 0;
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<Category> GetCategoryByCategoryId(int categoryId, bool withPosts)
        {
            Category categoryToGet = null;

            if (withPosts == true)
            {
                categoryToGet = await _appDbContext.Categories
                    .Include(category => category.Posts)
                    .FirstAsync(category => category.CategoryId == categoryId);
            }
            else
            {
                categoryToGet = await _appDbContext.Categories
                    .FirstAsync(category => category.CategoryId == categoryId);
            }

            return categoryToGet;
        }

        #endregion
    }
}

