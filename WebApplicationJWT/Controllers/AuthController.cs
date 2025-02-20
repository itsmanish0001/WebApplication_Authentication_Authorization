using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationJWT.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (model.Username == "manish" && model.Password == "manish123")
            {
                var token = _jwtTokenService.GenerateToken(model.Username, "Admin");

                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true, // 🔹 Prevent JavaScript access (for security)
                    Secure = true,   // 🔹 Only allow HTTPS
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddMinutes(30) // Set expiration
                });

                return Ok(new { message = "Login successful!" });
            }
            return Unauthorized("Invalid credentials");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("AuthToken", "", new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Expire immediately
                HttpOnly = true,
                Secure = true
            });

            return Ok(new { message = "Logged out successfully" });
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
