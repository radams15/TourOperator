using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private UserDAO _userDao;
    
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    private readonly ILogger<HomeController> _logger;
    IConfiguration? Configuration { get; }

    public AuthController(IConfiguration configuration, ILogger<HomeController> logger)
    {
        _logger = logger;
        Configuration = configuration;
        _userDao = new(configuration.GetConnectionString("DefaultConnection"));
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm] Credentials creds)
    {
        string connectionString = Configuration?["ConnectionStrings:DefaultConnection"] ?? "";
        SqlConnection connection = new SqlConnection(connectionString);

        _logger.Log(LogLevel.Error, $"Creds: {creds.Username} {creds.Password}");
        
        return Ok("Success");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return Problem("Error");
    }
}
