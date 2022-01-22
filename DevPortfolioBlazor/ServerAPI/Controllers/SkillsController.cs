using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SkillsController : ControllerBase
    {
        public SkillsController(AppDbContext dbContext)
        {
            this._appDbContext = dbContext;
        }

        public AppDbContext _appDbContext { get; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var skills = await _appDbContext.Skills.OrderBy(s => s.DisplayOrder).ToListAsync();

            return Ok(skills);
        }
    }
}
