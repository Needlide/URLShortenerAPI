﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            repository.Delete(url);

            return Ok("Deleted successfully");
        }

        [HttpPost("/add")]
        public IActionResult Add([FromBody] LongUrl url)
        {
            string protocol = url.Url.Split(':')[0] + "://";

            string domain = DomainExtractor.ExtractDomain(url.Url);

            string path = url.Url[(protocol.Length + domain.Length)..];

            StringBuilder shortUrl = new(protocol);

            shortUrl.Append(domain);
            shortUrl.Append('/');

            Random random = new();

            string shortPath = shortener.GenerateShortString(path.GetHashCode() + random.Next(10, 1000));

            shortUrl.Append(shortPath);

            repository.Add(new UrlEntry { OriginalUrl = url.Url, ShortenedUrl = shortUrl.ToString(), UserId = url.UserId });

            return Ok("Added successfully");
        }
    }
}
