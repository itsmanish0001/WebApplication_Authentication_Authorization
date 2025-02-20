using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/secure")]
[ApiController]
[Authorize] // 🔐 Requires JWT Authentication
public class SecureController : ControllerBase
{
    [HttpGet]
    public IActionResult GetSecureData()
    {
        return Ok(new { message = "You are authenticated!" });
    }

    [HttpGet("admin")]
    [Authorize(Roles = "Admin")] // 🔐 Only Admins can access this
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "Hello, Admin!" });
    }
}
