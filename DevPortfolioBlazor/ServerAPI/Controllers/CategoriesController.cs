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

        public CategoriesController(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _appDbContext.Categories.ToListAsync();

            return Ok(categories);
        }
    }
}
