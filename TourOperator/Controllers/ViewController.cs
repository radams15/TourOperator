using System.Collections;
using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TourOperator.Contexts;
using TourOperator.Extensions;
using TourOperator.Models;
using TourOperator.Models.Entities;
using TourOperator.Models.Services;

namespace TourOperator.Controllers;

[ApiController]
[Route("/")]
public class ViewController : Controller
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;
    private readonly AvailabilityService _availabilitySvc;

    public ViewController(TourDbContext tourDbContext, ILogger<ViewController> logger)
    {
        _tourDbContext = tourDbContext;
        _logger = logger;
        _availabilitySvc = new AvailabilityService(_tourDbContext);
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
    public IActionResult HotelSearch()
    {
        return View("HotelSearch");
    }
    
    [HttpPost("Hotels")]
    public IActionResult Hotels([FromForm] string? from, [FromForm] string? to)
    {
        if (from == null || to == null)
        {
            return Problem("FromDate and ToDate must be specified");
        }

        ViewBag.Message = new Hashtable
        {
            { "fromDate", from },
            { "toDate", to }
        };

        IEnumerable<Hotel> hotels = _availabilitySvc.HotelsBetweenDates(
            DateTime.ParseExact(from, "yyyy-MM-dd", CultureInfo.InvariantCulture),
            DateTime.ParseExact(to, "yyyy-MM-dd", CultureInfo.InvariantCulture)
        );

        return View(hotels);
    }

    [HttpGet("Hotel/{hotelId}")]
    public IActionResult Hotel(int hotelId, [FromQuery] string fromDate, [FromQuery] string toDate)
    {
        Hotel? hotel = _tourDbContext.Hotels
            .Include(h => h.Rooms)
            .FirstOrDefault(h => h.Id == hotelId);

        if (hotel == null )
        {
            return Problem($"No such hotel: {hotelId}");
        }
        
        ViewBag.Message = new Hashtable
        {
            { "fromDate", fromDate },
            { "toDate", toDate }
        };

        return View(hotel);
    }
    
    [HttpGet("Checkout")]
    [Authorize]
    public IActionResult Checkout()
    {
        RoomBooking roomBooking = new RoomBooking
        {
            Room = HttpContext.Session.GetObject<Room>("PackageRoom"),
            DateFrom = HttpContext.Session.GetObject<DateTime>("RoomDateFrom"),
            DateTo = HttpContext.Session.GetObject<DateTime>("RoomDateTo"),
        };
        
        Booking booking = new Booking
        {
            Customer = _tourDbContext.Customers.Find(User.Identity!.Name),
            RoomBooking = roomBooking,
            TourBooking = null
        };
        
        return View(booking);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
