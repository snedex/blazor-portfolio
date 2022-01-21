using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        public ProjectsController(AppDbContext dbContext)
        {
            this._appDbContext = dbContext;
        }

        public AppDbContext _appDbContext { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _appDbContext.Projects.AsNoTracking()
                .Include(p => p.Detail)
                .ThenInclude(d => d.Images)
                .OrderBy(p => p.ProjectId)
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> Details()
        {
            var details = await _appDbContext.ProjectDetails.AsNoTracking()
                .Include(d => d.Images)
                .OrderBy(d => d.ProjectId)
                .ToListAsync();

            return Ok(details);
        }

        [HttpGet("Detail")]
        public async Task<IActionResult> Detail(int? id)
        {
            var detail = await _appDbContext.ProjectDetails.AsNoTracking()
                .Include(d => d.Images)
                .Where(d => d.ProjectId == id)
                .FirstOrDefaultAsync();

            return Ok(detail);
        }
    }
}
