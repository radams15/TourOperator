using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourOperator.Contexts;
using TourOperator.Extensions;
using TourOperator.Models;
using TourOperator.Models.Entities;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : Controller
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;

    public AuthController(TourDbContext tourDbContext, ILogger<ViewController> logger)
    {
        _tourDbContext = tourDbContext;
        _logger = logger;
    }

    [HttpGet("/logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        _logger.LogInformation("User {} logged out.", User.Identity.Name);
        
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        
        return Redirect("/");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromForm] string username, [FromForm] string password)
    {
        Customer? existing = _tourDbContext.Customers.Find(username);

        if (existing == null)
            goto INVALID_PASSWORD;

        string attemptHash = password.Sha256();

        if (existing.Password != attemptHash)
            goto INVALID_PASSWORD;

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, existing.Username),
            new (ClaimTypes.Role, RoleName.Customer),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            
        };
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties
        );
        
        _logger.LogInformation("User {} logged in.", existing.Username);
        
        return Redirect("/");
        
INVALID_PASSWORD:
        _logger.LogInformation("Failed login, User {}.", username);
        return Problem("Invalid username or password");
    }
    
    [HttpPost("register")]
    public ActionResult<Customer> Register([FromForm] string username, [FromForm] string password, [FromForm] string password2)
    {
        if (password != password2)
            return Problem("Password 1 != Password 2");

        string passwordHash = password.Sha256();
        
        Customer customer = new()
        {
            Username = username,
            Password = passwordHash
        };
        
        _tourDbContext.Customers.Add(customer);
        _tourDbContext.SaveChanges();
        
        _logger.LogInformation("User {} created.", customer.Username);

        Customer? createdCustomer = _tourDbContext.Customers.Find(customer.Username);

        if (createdCustomer != null)
            return Ok(createdCustomer);
        
        return Problem("Failed to create customer");
    }


    [HttpPost("room/addToPackage")]
    [Authorize]
    public ActionResult AddRoomToPackage([FromForm] int roomId, [FromForm] string fromDate, [FromForm] string toDate)
    {
        Room? room = _tourDbContext.Rooms.Find(roomId);
        
        if (room == null)
            return Problem($"Could not find room {roomId}");
        
        room.FromDate = DateTime.ParseExact(fromDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        room.ToDate = DateTime.ParseExact(toDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

        HttpContext.Session.SetObject("PackageRoom", room);
        
        return Redirect($"/Hotel/{room.HotelId}?fromDate={fromDate}&toDate={toDate}");
    }
}
