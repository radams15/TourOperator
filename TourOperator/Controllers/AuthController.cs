using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourOperator.Contexts;
using TourOperator.Models;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;
    
    public class LoginCredentials
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    
    public class RegisterCredentials
    {
        public string username { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
    }

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
    public async Task<ActionResult> Login([FromForm] LoginCredentials creds)
    {
        Customer? existing = _tourDbContext.Customers.Find(creds.username);

        if (existing == null)
            goto INVALID_PASSWORD;

        string attemptHash = Sha256(creds.password);

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
        _logger.LogInformation("Failed login, User {}.", creds.username);
        return Problem("Invalid username or password");
    }
    
    [HttpPost("register")]
    public ActionResult<Customer> Register([FromForm] RegisterCredentials creds)
    {
        if (creds.password != creds.password2)
            return Problem("Password 1 != Password 2");

        string passwordHash = Sha256(creds.password);
        
        Customer customer = new()
        {
            Username = creds.username,
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

    private ActionResult AddBasketItem(BasketItem item, string referrer)
    {
        _tourDbContext.BasketItems.Add(item);
        _tourDbContext.SaveChanges();
        _logger.LogDebug("Basket Item Id: {}", item.Id);

        return Redirect(referrer);
    }
    
    [HttpPost("basket/addRoom")]
    [Authorize]
    public ActionResult AddRoom([FromForm] int roomId, [FromForm] string referrer)
    {
        Room? room = _tourDbContext.Rooms.Find(roomId);
        if (room == null)
            return Problem("Failed to find room");

        BasketItem item = new BasketItem
        {
            Username = User.Identity.Name,
            RoomId = roomId
        };
        
        return AddBasketItem(item, referrer);
    }
    
    [HttpPost("basket/addTour")]
    [Authorize]
    public ActionResult AddTour([FromForm] int tourId, [FromForm] string referrer)
    {
        Tour? tour = _tourDbContext.Tours.Find(tourId);
        if (tour == null)
            return Problem("Failed to find tour");

        BasketItem item = new BasketItem
        {
            Username = User.Identity.Name,
            TourId = tourId
        };

        return AddBasketItem(item, referrer);
    }
    
    private static string Sha256(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            
            return builder.ToString();
        }
    }

}
