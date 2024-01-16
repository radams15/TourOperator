namespace TourOperator.Controllers;

using System.Security.Claims;
using Contexts;
using Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    /// <summary>
    /// Logs out the user by deleting their cookie from the browser.
    /// </summary>
    [HttpGet("logout")]
    [Authorize]
    public async Task<ActionResult> Logout()
    {
        _logger.LogInformation("User {} logged out.", User.Identity.Name);

        await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);

        return Redirect("/");
    }
    
    /// <summary>
    /// GET handler for the login page
    /// </summary>
    /// <returns>Login page view</returns>
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// POST handler for login endpoint.
    /// Authenticates user, then if username and password correct gives the user a cookie with their
    /// claims, inc. roles extracted from the database.
    /// </summary>
    /// <param name="username">Username as string</param>
    /// <param name="password">Password as string</param>
    /// <returns>Success: Redirect to home page. Failure: Invalid password message.</returns>
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromForm] string username, [FromForm] string password)
    {
        Customer? existing = _tourDbContext.Customers
            .Include(c => c.Role)
            .SingleOrDefault(c => c.Username == username);

        // Username not found
        if (existing == null)
            goto INVALID_PASSWORD;

        string attemptHash = password.Sha256();

        // Password incorrect
        if (existing.Password != attemptHash)
            goto INVALID_PASSWORD;

        // User claims of name and role can be accessed from the asp.net `User` object
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, existing.Username),
            new Claim(ClaimTypes.Role, existing.Role.Name),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            
        };
        
        // Give the user an authentication cookie.
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity), 
            authProperties
        );
        
        _logger.LogInformation("User {} logged in.", existing.Username);
        
        return Redirect("/");
        
        // Common invalid password handler. Common handlers are the only useful application of GOTO, especially
        // here where the usage is explicitly clear.
INVALID_PASSWORD:
        _logger.LogInformation("Failed login, User {}.", username);
        return Problem("Invalid username or password");
    }
    
    /// <summary>
    /// GET handler for register page
    /// </summary>
    /// <returns>Register page with a new, blank customer object</returns>
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View(new Customer());
    }
    
    /// <summary>
    /// POST handler for register page
    /// </summary>
    /// <param name="customer">Customer object with all fields but role filled in</param>
    /// <returns>Success: redirect to home page. Failure: redirect back to register page with errors shown.</returns>
    [HttpPost("register")]
    public ActionResult<Customer> Register([FromForm] Customer customer)
    {
        ModelState.Remove(ModelState.Keys.First(key => key == "Role"));
        
        // If all model validations successful
        if (ModelState.IsValid) {
            // Extract role object of customer
            customer.Role = _tourDbContext.Roles
                .Where(r => r.Name == RoleName.Customer)
                .SingleOrDefault();
            
            customer.Password = customer.Password.Sha256();

            // Add the customer object to the db
            _tourDbContext.Customers.Add(customer);
            _tourDbContext.SaveChanges();

            _logger.LogInformation("User {} created.", customer.Username);

            // TODO: Log in the user
            Customer? createdCustomer = _tourDbContext.Customers.Find(customer.Id);

            if (createdCustomer != null)
                return Redirect("/");
            
            return Problem("Failed to create customer");
        }
        
        // Log all errors to server console
        ModelState.Select(x => x.Value.Errors)
            .Where(y=>y.Count>0)
            .ToList()
            .ForEach(error => {
                _logger.LogError("Registration validation error: {0}", error.Select(x => x.ErrorMessage));
            });

        return View(customer);
    }


    /// <summary>
    /// Add a room to the package, using the session cookie.
    /// </summary>
    /// <param name="roomId">Id of the room to add</param>
    /// <param name="fromDate">Start date as yyyy-mm-dd</param>
    /// <param name="toDate">End date as yyyy-mm-dd</param>
    /// <returns>Success: Redirect to the page of the room hotel. Error: Error message when room id invalid</returns>
    [HttpPost("room/addToPackage")]
    [Authorize]
    public ActionResult AddRoomToPackage([FromForm] int roomId, [FromForm] string fromDate, [FromForm] string toDate)
    {
        Room? room = _tourDbContext.Rooms.Find(roomId);
        
        if (room == null)
            return Problem($"Could not find room {roomId}");

        // Set the cookies
        HttpContext.Session.SetObject("PackageRoom", room);
        HttpContext.Session.SetObject("RoomDateTo", toDate.ParseDate());
        HttpContext.Session.SetObject("DateFrom", fromDate.ParseDate());
        
        return Redirect($"/Hotel/{room.HotelId}?fromDate={fromDate}&toDate={toDate}");
    }
    
    /// <summary>
    /// Add a tour to the package, using the session cookie.
    /// </summary>
    /// <param name="tourId">Id of the tour to add</param>
    /// <param name="fromDate">Start date as yyyy-mm-dd</param>
    /// <returns>Success: Redirect to the page of the tour. Error: Error message when tour id invalid</returns>
    [HttpPost("tour/addToPackage")]
    [Authorize]
    public ActionResult AddTourToPackage([FromForm] int tourId, [FromForm] string fromDate)
    {
        Tour? tour = _tourDbContext.Tours.Find(tourId);
        
        if (tour == null)
            return Problem($"Could not find tour {tourId}");
        
        // Set the cookies
        HttpContext.Session.SetObject("PackageTour", tour);
        HttpContext.Session.SetObject("DateFrom", fromDate.ParseDate());
        
        return Redirect($"/Tour/{tour.Id}?fromDate={fromDate}");
    }
}
