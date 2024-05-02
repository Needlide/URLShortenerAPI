using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
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
            var entries = repository.GetAll().Select(entry => new UrlEntryDto
            {
                Id = entry.Id,
                OriginalUrl = entry.OriginalUrl,
                ShortenedUrl = entry.ShortenedUrl,
            }).ToList();

            return Ok(entries);
        }

        [HttpPost("/info")]
        public IActionResult Get([FromBody] int id)
        {
            return Ok(repository.GetById(id));
        }

        [HttpDelete("/delete")]
        public IActionResult Delete([FromBody] UrlEntry url)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return BadRequest("Problem with token. Try to login again");
            }

            if (userIdClaim.Value != url.UserId.ToString())
            {
                return Unauthorized("You are not authorized to delete this record");
            }

            repository.Delete(url);

            return Ok("Deleted successfully");
        }

        [HttpPost("/add")]
        public IActionResult Add([FromBody] string url)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return BadRequest("Problem with token. Try to login again");
            }

            string protocol = url.Split(':')[0] + "://";

            string domain = DomainExtractor.ExtractDomain(url);

            string path = url[(protocol.Length + domain.Length)..];

            StringBuilder shortUrl = new(protocol);

            shortUrl.Append(domain);
            shortUrl.Append('/');

            Random random = new();

            string shortPath = shortener.GenerateShortString(path.GetHashCode() + random.Next(10, 100));

            shortUrl.Append(shortPath);

            repository.Add(new UrlEntry { OriginalUrl = url, ShortenedUrl = shortUrl.ToString(), UserId = Convert.ToInt32(userIdClaim.Value) });

            return Ok("Added successfully");
        }

        [HttpGet("/getUsers")]
        public IActionResult GetUsersUrls()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return BadRequest("Problem with token. Try to login again");
            }

            var usersUrls = repository.GetAll().Where(x => x.UserId == Convert.ToInt32(userIdClaim.Value)).ToList();

            return Ok(usersUrls);
        }
    }
}
