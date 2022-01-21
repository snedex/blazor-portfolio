using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Data;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public PostsController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Post> posts = await _appDbContext.Posts
                .Include(post => post.Category)
                .ToListAsync();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Post post = await GetPostByPostId(id);

            return Ok(post);
        }

        [HttpGet("dto/{id}")]
        public async Task<IActionResult> GetDTO(int id)
        {
            Post post = await GetPostByPostId(id);
            var postDTO = _mapper.Map<PostViewModel>(post);

            return Ok(postDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostViewModel newPost)
        {
            try
            {
                if (newPost == null)
                {
                    return BadRequest(ModelState);
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Post postToCreate = _mapper.Map<Post>(newPost);

                if (postToCreate.Published == true)
                {
                    // European DateTime
                    postToCreate.PublishDate = DateTime.UtcNow.ToString(Helpers.c_SysDateTimeFormat);
                }

                await _appDbContext.Posts.AddAsync(postToCreate);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, Helpers.c_HTTP500Message_Short);
                }
                else
                {
                    return Created("Create", postToCreate);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{Helpers.c_HTTP500Message_Long} {e.Message}.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostViewModel updatePost)
        {
            try
            {
                if (id < 1 || updatePost == null || id != updatePost.PostId)
                {
                    return BadRequest(ModelState);
                }

                Post oldPost = await _appDbContext.Posts.FindAsync(id);

                if (oldPost == null)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Post updatedPost = _mapper.Map<Post>(updatePost);

                if (updatedPost.Published == true)
                {
                    if (oldPost.Published == false)
                    {
                        updatedPost.PublishDate = DateTime.UtcNow.ToString(Helpers.c_SysDateTimeFormat);
                    }
                    else
                    {
                        updatedPost.PublishDate = oldPost.PublishDate;
                    }
                }
                else
                {
                    updatedPost.PublishDate = string.Empty;
                }

                // Detach oldPost from EF, else it can't be updated.
                _appDbContext.Entry(oldPost).State = EntityState.Detached;

                _appDbContext.Posts.Update(updatedPost);

                bool changesPersistedToDatabase = await PersistChangesToDatabase();

                if (changesPersistedToDatabase == false)
                {
                    return StatusCode(500, Helpers.c_HTTP500Message_Short);
                }
                else
                {
                    return Created("Create", updatedPost);
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

                bool exists = await _appDbContext.Posts.AnyAsync(post => post.PostId == id);

                if (exists == false)
                {
                    return NotFound();
                }

                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                Post postToDelete = await GetPostByPostId(id);

                if (postToDelete.ThumbnailImagePath != "uploads/placeholder.jpg")
                {
                    string fileName = postToDelete.ThumbnailImagePath.Split('/').Last();

                    System.IO.File.Delete($"{_webHostEnvironment.ContentRootPath}\\wwwroot\\uploads\\{fileName}");
                }

                _appDbContext.Posts.Remove(postToDelete);

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
        private async Task<Post> GetPostByPostId(int postId)
        {
            Post postToGet = await _appDbContext.Posts
                    .Include(post => post.Category)
                    .FirstAsync(post => post.PostId == postId);

            return postToGet;
        }

        #endregion
    }
}
