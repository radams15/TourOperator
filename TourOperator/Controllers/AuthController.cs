using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    public class Credentials
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    
    private readonly ILogger<HomeController> _logger;
    IConfiguration? Configuration { get; }

    public AuthController(IConfiguration configuration, ILogger<HomeController> logger)
    {
        _logger = logger;
        Configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm] Credentials creds)
    {
        string connectionString = Configuration?["ConnectionStrings:DefaultConnection"] ?? "";
        SqlConnection connection = new SqlConnection(connectionString);

        _logger.Log(LogLevel.Error, $"Creds: {creds.username} {creds.password}");
        
        return Ok("Success");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return Problem("Error");
    }
}
