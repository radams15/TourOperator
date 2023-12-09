using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TourOperator.Models;
using TourOperator.Repository;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class ViewController : Controller
{
    private readonly ILogger<ViewController> _logger;
    IConfiguration? Configuration { get; }

    private TourRepository _tourRepository;
    private CustomerRepository _customerRepository;
    private OperatorRepository _operatorRepository;
    private HotelRepository _hotelRepository;
    private RoomRepository _roomRepository;
    private BookingRepository _bookingRepository;

    public ViewController(IConfiguration _configuration, ILogger<ViewController> logger)
    {
        _logger = logger;
        Configuration = _configuration;
        
        string sqlConnectionString = _configuration.GetConnectionString("DefaultConnection")
                                     ?? throw new NullReferenceException("SQL connection string cannot be null");
        
        _tourRepository = new TourRepository(sqlConnectionString);
        _customerRepository = new CustomerRepository(sqlConnectionString);
        _operatorRepository = new OperatorRepository(sqlConnectionString);
        _hotelRepository = new HotelRepository(sqlConnectionString);
        _roomRepository = new RoomRepository(sqlConnectionString);
        _bookingRepository = new BookingRepository(sqlConnectionString);
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
