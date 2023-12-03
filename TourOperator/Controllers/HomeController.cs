using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    IConfiguration? Configuration { get; }

    public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
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

    [HttpGet("privacy")]
    public IActionResult Privacy()
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
