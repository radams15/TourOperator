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
    
    [HttpGet("Tours")]
    public IActionResult TourSearch()
    {
        return View("TourSearch");
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
            from.ParseDate(),
            to.ParseDate()
        );

        return View(hotels);
    }
    
    [HttpPost("Tours")]
    public IActionResult Tours([FromForm] string? from)
    {
        if (from == null)
        {
            return Problem("From date must be specified");
        }

        ViewBag.Message = new Hashtable
        {
            { "fromDate", from }
        };

        IEnumerable<Tour> tours = _availabilitySvc.ToursBetweenDates(
            from.ParseDate()
        );

        return View(tours);
    }

    [HttpGet("Hotel/{hotelId}")]
    public IActionResult Hotel(int hotelId, [FromQuery] string fromDate, [FromQuery] string toDate)
    {
        Hotel? hotel = _tourDbContext.Hotels
            .Include(h => h.Rooms)
                .ThenInclude(r => r.Bookings)
            .FirstOrDefault(h => h.Id == hotelId);

        if (hotel == null )
        {
            return Problem($"No such hotel: {hotelId}");
        }
        
        hotel.Rooms = _availabilitySvc
            .RoomsBetweenDates(hotel, fromDate.ParseDate(), toDate.ParseDate())
            .ToList();
        
        ViewBag.Message = new Hashtable
        {
            { "fromDate", fromDate },
            { "toDate", toDate }
        };

        return View(hotel);
    }
    
    [HttpGet("Tour/{tourId}")]
    public IActionResult Tour(int tourId, [FromQuery] string fromDate)
    {
        Tour? tour = _tourDbContext.Tours
            .Include(t => t.Bookings)
            .FirstOrDefault(t => t.Id == tourId);

        if (tour == null)
        {
            return Problem($"No such tour: {tourId}");
        }
        
        ViewBag.Message = new Hashtable
        {
            { "fromDate", fromDate }
        };

        return View(tour);
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
        roomBooking.RoomId = roomBooking.Room!.Id;
        
        TourBooking tourBooking = new TourBooking
        {
            Tour = HttpContext.Session.GetObject<Tour>("PackageTour"),
            DateFrom = HttpContext.Session.GetObject<DateTime>("RoomDateFrom"),
        };
        tourBooking.TourId = tourBooking.Tour!.Id;
        
        Booking booking = new Booking
        {
            Username = User.Identity!.Name,
            RoomBooking = roomBooking,
            TourBooking = tourBooking,
            TotalCost = 0
        };

        if (roomBooking.Room != null)
        {
            int days = roomBooking.DateTo.Subtract(roomBooking.DateFrom).Days;
            booking.TotalCost += roomBooking.Room.Price * days;
        }

        if (tourBooking.Tour != null)
        {
            booking.TotalCost += tourBooking.Tour.Price;
        }

        if (roomBooking.Room != null && tourBooking.Tour != null)
        {
            
        }
        
        return View(booking);
    }
    
    [HttpGet("booking/confirmed")]
    public IActionResult BookingConfirmed([FromQuery] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.Customer)
            .Include(b => b.RoomBooking!.Room)
                .ThenInclude(r => r!.Hotel)
            .Include(b => b.TourBooking!.Tour)
            .FirstOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        if(booking.Customer!.Username != User.Identity!.Name)
            return Problem($"Cannot load booking not for user {User.Identity!.Name}");
        
        return View(booking);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
