using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class ViewController : Controller
{
    private readonly ILogger<ViewController> _logger;
    IConfiguration? Configuration { get; }

    public ViewController(IConfiguration configuration, ILogger<ViewController> logger)
    {
        _logger = logger;
        Configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string connectionString = Configuration?["ConnectionStrings:DefaultConnection"] ?? "";
        SqlConnection connection = new SqlConnection(connectionString);

        return View();
    }
    
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpGet("customer")]
    [Authorize]
    public IActionResult Customer()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
