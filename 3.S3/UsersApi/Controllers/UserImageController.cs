using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Users.Api.Services;

namespace Users.Api.Controllers
{
    public class UserImageController : Controller
    {
        private readonly IUserImageService _userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }

        [HttpPost("users/{id:int}/image")]
        public async Task<IActionResult> Upload([FromRoute] int id,
        [FromForm(Name = "Data")] IFormFile file)
        {
            var response = await _userImageService.UploadImageAsync(id, file);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("users/{id:int}/image")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var response = await _userImageService.GetImageAsync(id);
                return File(response.ResponseStream, response.Headers.ContentType);
            }
            catch (AmazonS3Exception ex) when (ex.Message is "The specified key does not exist.")
            {
                return NotFound();
            }
        }

        [HttpDelete("users/{id:int}/image")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _userImageService.DeleteImageAsync(id);
            return response.HttpStatusCode switch
            {
                HttpStatusCode.NoContent => Ok(),
                HttpStatusCode.NotFound => NotFound(),
                _ => BadRequest()
            };
        }
    }
}
