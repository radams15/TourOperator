using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TourOperator.Models;
using TourOperator.Repository;

namespace TourOperator.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase
{
    private CustomerRepository _customerRepository;
    private BookingRepository _bookingRepository;
    private readonly ILogger<ViewController> _logger;
    private IConfiguration? _configuration;

    
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

    public AuthController(IConfiguration configuration, ILogger<ViewController> logger)
    {
        _logger = logger;
        _configuration = configuration;

        string sqlConnectionString = _configuration.GetConnectionString("DefaultConnection")
                                     ?? throw new NullReferenceException("SQL connection string cannot be null");
        
        _customerRepository = new CustomerRepository(sqlConnectionString);
        _bookingRepository = new BookingRepository(sqlConnectionString);
    }

    [HttpGet("/logout")]
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
        Customer? existing = _customerRepository.GetCustomer(creds.username);

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
        
        Customer? createdCustomer = _customerRepository.CreateCustomer(customer);

        if (createdCustomer != null)
            return Ok(createdCustomer);
        
        return Problem("Failed to create customer");
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
