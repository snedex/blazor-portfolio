using Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using IO = System.IO;
using System;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        //TODO Magic strings
        private const string c_RelativeRoot = "uploads/";
        private const string c_AbsoluteRoot = "wwwroot\\uploads";
        private const string c_DefaultPlaceholder = "placeholder.jpg";

        public UploadImageController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        private IWebHostEnvironment _hostEnvironment { get; }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageViewModel uploadedImage)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                if (!string.IsNullOrEmpty(uploadedImage.OldImagePath))
                {
                    if(!uploadedImage.OldImagePath.Contains(c_DefaultPlaceholder))
                    {
                        string oldFileName = uploadedImage.OldImagePath.Split('/').Last();
                        string fileName = Path.Combine(_hostEnvironment.ContentRootPath, c_AbsoluteRoot, oldFileName);

                        if(IO.File.Exists(fileName))
                            IO.File.Delete(fileName);
                    }
                }

                string imageFileName = string.Concat(Guid.NewGuid().ToString(), uploadedImage.FileExtension);

                string fullImageFileSystemPath = Path.Combine(_hostEnvironment.ContentRootPath, 
                    c_AbsoluteRoot, imageFileName);

                using(var fs = IO.File.Create(fullImageFileSystemPath))
                {
                    byte[] imgBytes = Convert.FromBase64String(uploadedImage.Base64Content);
                    await fs.WriteAsync(imgBytes);
                }

                string serverImageLocation = $"{c_RelativeRoot}{imageFileName}";
                return Created("Create", serverImageLocation);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{Helpers.c_HTTP500Message_Long} {e.Message}");
            }
        }
    }
}
