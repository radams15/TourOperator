using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Contexts;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class ViewController : Controller
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;

    public ViewController(TourDbContext tourDbContext, ILogger<ViewController> logger)
    {
        _tourDbContext = tourDbContext;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
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
    
    [HttpGet("Hotels")]
    [Authorize]
    public IActionResult Hotels()
    {
        ViewBag.Message = new Hashtable{{"tourDb", _tourDbContext}};
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
