using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using URLShortenerAPI.Abstract;
using URLShortenerAPI.Database;
using URLShortenerAPI.Models;
using URLShortenerAPI.Security;

namespace URLShortenerAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController(IRepository<User> repository, UserContext context) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("/registration")]
        public IActionResult Registration([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                user.Password = BCryptHash.Hash(user.Password);

                repository.Add(user);

                string jwt = GenerateJwt(user.Login);

                return Ok(new { message = "Registered successfully", token = "Bearer " + jwt });
            }
            catch (DbUpdateException) { return StatusCode(500); }
            catch (ArgumentException ex) { return BadRequest(ex); }
            catch (SaltParseException) { return StatusCode(500); }
            catch (Exception) { return StatusCode(500, "An unexpected error occured."); }
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                User? dbUser = repository.GetAll().FirstOrDefault(x => x.Login == user.Login);

                if (dbUser == null)
                    return BadRequest("Invalid credentials");

                if (!BCryptHash.Verify(user.Password, dbUser.Password))
                    return BadRequest("Invalid credentials");

                string jwt = GenerateJwt(user.Login);

                return Ok(new { message = "Login successful", token = "Bearer " + jwt });
            }
            catch (ArgumentException ex) { return BadRequest(ex); }
            catch (InvalidOperationException ex) { return StatusCode(500); }
            catch (SaltParseException) { return StatusCode(500); }
            catch (SecurityTokenEncryptionFailedException) { return StatusCode(500); }
            catch (Exception) { return StatusCode(500, "An unexpected error occured."); }
        }

        private string GenerateJwt(string login)
        {
            int userId = repository.GetAll().First(x => x.Login == login).Id;

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, login),
                    new(ClaimTypes.NameIdentifier, userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "shortenerapi",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("_secret_key_long_key_even_longer_secret_key_")), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
