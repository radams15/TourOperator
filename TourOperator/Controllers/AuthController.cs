using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;
    IConfiguration? Configuration { get; }

    public AuthController(IConfiguration configuration, ILogger<HomeController> logger)
    {
        _logger = logger;
        Configuration = configuration;
    }

    [HttpGet("/login")]
    public IActionResult Login()
    {
        string connectionString = Configuration?["ConnectionStrings:DefaultConnection"] ?? "";
        SqlConnection connection = new SqlConnection(connectionString);

        return Ok("Success");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return Problem("Error");
    }
}
