namespace TourOperator.Controllers;

using System.Security.Claims;
using Contexts;
using Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

[Controller]
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

    [HttpGet("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        _logger.LogInformation("User {} logged out.", User.Identity.Name);

        await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/");
    }
    
        
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromForm] string username, [FromForm] string password)
    {
        Customer? existing = _tourDbContext.Customers.SingleOrDefault(c => c.Username == username);

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
    
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new Customer());
    }
    
    [HttpPost("register")]
    public ActionResult<Customer> Register([FromForm] Customer customer)
    {
        if (ModelState.IsValid)
        {

            customer.Password = customer.Password.Sha256();

            _tourDbContext.Customers.Add(customer);
            _tourDbContext.SaveChanges();

            _logger.LogInformation("User {} created.", customer.Username);

            Customer? createdCustomer = _tourDbContext.Customers.Find(customer.Id);

            if (createdCustomer != null)
                return Redirect("/");
            
            return Problem("Failed to create customer");
        }

        return View(customer);
    }


    [HttpPost("room/addToPackage")]
    [Authorize]
    public ActionResult AddRoomToPackage([FromForm] int roomId, [FromForm] string fromDate, [FromForm] string toDate)
    {
        Room? room = _tourDbContext.Rooms.Find(roomId);
        
        if (room == null)
            return Problem($"Could not find room {roomId}");

        HttpContext.Session.SetObject("RoomDateTo", toDate.ParseDate());
        HttpContext.Session.SetObject("DateFrom", fromDate.ParseDate());

        HttpContext.Session.SetObject("PackageRoom", room);
        
        return Redirect($"/Hotel/{room.HotelId}?fromDate={fromDate}&toDate={toDate}");
    }
    
    [HttpPost("tour/addToPackage")]
    [Authorize]
    public ActionResult AddTourToPackage([FromForm] int tourId, [FromForm] string fromDate)
    {
        Tour? tour = _tourDbContext.Tours.Find(tourId);
        
        if (tour == null)
            return Problem($"Could not find tour {tourId}");
        
        HttpContext.Session.SetObject("PackageTour", tour);
        HttpContext.Session.SetObject("DateFrom", fromDate.ParseDate());
        
        return Redirect($"/Tour/{tour.Id}?fromDate={fromDate}");
    }
}
