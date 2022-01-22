using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ProjectsController : ControllerBase
    {
        public ProjectsController(AppDbContext dbContext)
        {
            this._appDbContext = dbContext;
        }

        public AppDbContext _appDbContext { get; }

        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Details()
        {
            var details = await _appDbContext.ProjectDetails.AsNoTracking()
                .Include(d => d.Images)
                .OrderBy(d => d.ProjectId)
                .ToListAsync();

            return Ok(details);
        }

        [HttpGet("Detail")]
        [AllowAnonymous]
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
