namespace TourOperator.Controllers;

using Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Services;

[Controller]
[Route("/booking")]
public class BookingController : Controller
{
    private readonly ILogger<ViewController> _logger;
    private readonly TourDbContext _tourDbContext;
    private readonly AvailabilityService _availabilitySvc;


    public BookingController(TourDbContext tourDbContext, ILogger<ViewController> logger)
    {
        _tourDbContext = tourDbContext;
        _logger = logger;
        _availabilitySvc = new AvailabilityService(_tourDbContext);
    }

    [HttpPost("deposit")]
    [Authorize]
    public ActionResult<Booking> MakeDeposit([FromForm] Booking booking)
    {
        booking.DepositPaid = true;
        booking.Customer = _tourDbContext.Customers.Single(c => c.Id == booking.CustomerId);
        booking.Due = (int) (booking.TotalCost * 0.8);
        booking.DateBooked = DateTime.Now;

        _tourDbContext.Bookings.Add(booking);

        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={booking.Id}");
    }
    
   
    [HttpGet("confirm")]
    [Authorize]
    public ActionResult<Booking> ConfirmBooking([FromQuery] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings.Find(bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");

        booking.Due = 0;
        
        _tourDbContext.Bookings.Update(booking);

        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={booking.Id}");
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
    
    [HttpGet("all")]
    [Authorize]
    public ActionResult AllBookings()
    {
        int customerId = CurrentCustomer().Id;
        
        IEnumerable<Booking> bookings = _tourDbContext.Bookings
            .Include(b => b.RoomBooking!.Room!.Hotel)
            .Include(b => b.TourBooking!.Tour)
            .Where(b => b.CustomerId == customerId);

        return View(bookings);
    }
    
    [HttpGet]
    public IActionResult BookingInfo([FromQuery] int bookingId)
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
    
    [HttpGet("edit")]
    [Authorize]
    public ActionResult<Booking> EditBooking([FromQuery] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.RoomBooking)
            .Include(b => b.TourBooking)
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        return View(booking);
    }
    
    [HttpPost("edit")]
    [Authorize]
    public ActionResult<Booking> CheckEditBooking([FromForm] int bookingId, [FromForm] DateTime from)
    {
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.RoomBooking)
                .ThenInclude(rb => rb.Room)
                    .ThenInclude(r => r.Hotel)
            .Include(b => b.TourBooking)
                .ThenInclude(tb => tb.Tour)
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");

        string error = "";
        
        if (booking.HasRoom()){
            int days = (booking.RoomBooking!.DateTo - booking.RoomBooking!.DateFrom).Days;
            DateTime newTo = from.AddDays(days);
            
            IEnumerable<Room> rooms = _availabilitySvc
                .RoomsBetweenDates(booking.RoomBooking!.Room!.Hotel, from, newTo)
                .Where(r => r.Id == booking.RoomBooking!.Room!.Id);

            if (rooms.Any()){
                booking.RoomBooking.DateFrom = from;
                booking.RoomBooking.DateTo = newTo;
            }
            else{
                error = "Room unavailable";
            }
        }

        if (booking.HasTour()){
            IEnumerable<Tour> tours = _availabilitySvc
                .ToursBetweenDates(from)
                .Where(t => t.Id == booking.TourBooking!.Tour!.Id);

            if (tours.Any()){
                booking.TourBooking!.DateFrom = from;
            }
            else{
                error = "Tour unavailable";
            }
        }

        booking.Due += (int) (booking.TotalCost * 0.05f); // 5% surcharge
        
        TempData["errormsg"] = error;
        
        return View("EditBooking", booking);
    }
    
    [HttpPost("update")]
    [Authorize]
    public ActionResult<Booking> ConfirmEditBooking([FromForm] int bookingId, [FromForm] DateTime from)
    {
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.RoomBooking)
            .ThenInclude(rb => rb.Room)
            .ThenInclude(r => r.Hotel)
            .Include(b => b.TourBooking)
            .ThenInclude(tb => tb.Tour)
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        if (booking.HasRoom()){
            int days = (booking.RoomBooking!.DateTo - booking.RoomBooking!.DateFrom).Days;
            DateTime newTo = from.AddDays(days);
            
            IEnumerable<Room> rooms = _availabilitySvc
                .RoomsBetweenDates(booking.RoomBooking!.Room!.Hotel, from, newTo)
                .Where(r => r.Id == booking.RoomBooking!.Room!.Id);

            if (rooms.Any()){
                booking.RoomBooking.DateFrom = from;
                booking.RoomBooking.DateTo = newTo;
            }
            else{
                return Problem("Room unavailable");
            }
        }

        if (booking.HasTour()){
            IEnumerable<Tour> tours = _availabilitySvc
                .ToursBetweenDates(from)
                .Where(t => t.Id == booking.TourBooking!.Tour!.Id);

            if (tours.Any()){
                booking.TourBooking!.DateFrom = from;
            }
            else{
                return Problem("Tour unavailable");
            }
        }
        
        _tourDbContext.Bookings.Update(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={bookingId}");
    }
    
    [HttpGet("cancel")]
    [Authorize]
    public ActionResult<Booking> CancelBooking([FromQuery] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.RoomBooking)
            .Include(b => b.TourBooking)
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        return View(booking);
    }
    
    [HttpPost("cancel")]
    [Authorize]
    public ActionResult<Booking> ConfirmCancelBooking([FromForm] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");

        booking.Cancelled = true;
        
        _tourDbContext.Bookings.Update(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={bookingId}");
    }
}
