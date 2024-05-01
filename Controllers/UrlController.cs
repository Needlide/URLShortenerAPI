using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLShortenerAPI.Abstract;
using URLShortenerAPI.Database;
using URLShortenerAPI.Models;
using URLShortenerAPI.Shared;

namespace URLShortenerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/content")]
    public class UrlController(IUrlRepository repository, UrlContext context, Shortener shortener) : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("/all")]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }

        [HttpGet("/info")]
        public IActionResult Get([FromBody] int id)
        {
            return Ok(repository.GetById(id));
        }

        [HttpDelete("/delete")]
        public IActionResult Delete([FromBody] UrlEntry url)
        {
            repository.Delete(url);

            return Ok("Deleted successfully");
        }

        [HttpPost("/add")]
        public IActionResult Add([FromBody] LongUrl url)
        {
            string shortUrl = url.Url.Split('/')[0] + url.Url.Split('/')[1] + url.Url.Split('/')[2] + shortener.GenerateShortString(Convert.ToInt32(url.Url.Split('/')[3]));

            repository.Add(new UrlEntry { OriginalUrl = url.Url, ShortenedUrl = shortUrl, UserId = url.UserId });

            return Ok("Added successfully");
        }
    }
}
