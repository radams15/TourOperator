using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private UserDao _userDao;
    
    public class LoginCredentials
    {
        public string Username { get; }
        public string Password { get; }
    }
    
    public class RegisterCredentials
    {
        public string Username { get; }
        public string Password { get; }
        public string Password2 { get; }
    }
    
    private readonly ILogger<HomeController> _logger;
    IConfiguration? Configuration { get; }

    public AuthController(IConfiguration configuration, ILogger<HomeController> logger)
    {
        _logger = logger;
        Configuration = configuration;
        _userDao = new UserDao(configuration.GetConnectionString("DefaultConnection"));
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm] LoginCredentials creds)
    {
        return Ok("Success");
    }
    
    [HttpPost("login")]
    public IActionResult Register([FromForm] RegisterCredentials creds)
    {
        if (creds.Password != creds.Password2)
            return Problem("Password 1 != Password 2");
        
        return Ok("Success");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return Problem("Error");
    }
}
