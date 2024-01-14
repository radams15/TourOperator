using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
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

[Controller]
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
    
    [HttpGet("customer")]
    [Authorize]
    public IActionResult Customer()
    {
        return View(CurrentCustomer());
    }
    
    [HttpPost("customer")]
    [Authorize]
    public ActionResult Customer([FromForm] Customer customer)
    {
        if (ModelState.IsValid)
        {
            Customer beforeCustomer = CurrentCustomer();
            
            beforeCustomer.FullName = customer.FullName;
            beforeCustomer.PhoneNo = customer.PhoneNo;
            beforeCustomer.PassportNo = customer.PassportNo;
            beforeCustomer.Password = customer.Password.Sha256();

            _tourDbContext.Customers.Update(beforeCustomer);
            _tourDbContext.SaveChanges();
            
            return View(customer);
        }
        
        foreach (var value in ModelState.Values)
        {
            if(value.Errors.Count > 0)
                foreach(var error in value.Errors)
                    _logger.LogError("Error: {}", error.ErrorMessage);
        }

        return View(customer);
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

    private Customer CurrentCustomer()
    {
        Customer customer = _tourDbContext.Customers
            .Where(c => c.Username == User.Identity!.Name)
            .FirstOrDefault() ;

        if (customer != null)
            return customer;

        throw new Exception("Invalid User");
    }
    
    [HttpGet("Checkout")]
    [Authorize]
    public IActionResult Checkout()
    {
        Room? room = HttpContext.Session.GetObject<Room>("PackageRoom");
        Tour? tour = HttpContext.Session.GetObject<Tour>("PackageTour");

        RoomBooking? roomBooking = null;
        TourBooking? tourBooking = null;

        if (room != null){
            roomBooking = new RoomBooking{
                Room = room, DateFrom = HttpContext.Session.GetObject<DateTime>("DateFrom"), DateTo = HttpContext.Session.GetObject<DateTime>("RoomDateTo"),
            };
            if (roomBooking.Room != null)
                roomBooking.RoomId = roomBooking.Room!.Id;
        }

        if (tour != null){
            tourBooking = new TourBooking{
                Tour = tour, DateFrom = HttpContext.Session.GetObject<DateTime>("DateFrom"),
            };
            if (tourBooking.Tour != null)
                tourBooking.TourId = tourBooking.Tour.Id;
        }

        Booking booking = new Booking
        {
            CustomerId = CurrentCustomer().Id,
            RoomBooking = roomBooking,
            TourBooking = tourBooking,
            TotalCost = 0
        };

        if (room != null && roomBooking != null)
        {
            int days = roomBooking.DateTo.Subtract(roomBooking.DateFrom).Days;
            booking.TotalCost += room.Price * days;
        }

        if (tour != null)
        {
            booking.TotalCost += tour.Price;
        }

        if (room != null && tour != null)
        {
            booking.TotalCost *= 1 - room.PackageDiscount / 100;
        }
        
        return View(booking);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
