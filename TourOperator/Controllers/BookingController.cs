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

    /// <summary>
    /// Make the 20% deposit to reserve a booking
    /// </summary>
    /// <param name="booking">Booking object to make deposit for</param>
    /// <returns>Redirect to the booking information page</returns>
    [HttpPost("deposit")]
    [Authorize]
    public ActionResult<Booking> MakeDeposit([FromForm] Booking booking)
    {
        // Set the booking information, set the due amount.
        booking.DepositPaid = true;
        booking.Customer = _tourDbContext.Customers.Single(c => c.Id == booking.CustomerId);
        booking.Due = (int) (booking.TotalCost * 0.8);
        booking.DateBooked = DateTime.Now;

        // Add to database bookings table.
        _tourDbContext.Bookings.Add(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={booking.Id}");
    }
    
   
    /// <summary>
    /// Confirm a booking by paying the full amount
    /// </summary>
    /// <param name="bookingId">Id of the booking to confirm</param>
    /// <returns>Success: redirect to the booking info page. Failure: Error message when bookingId is invalid</returns>
    [HttpGet("confirm")]
    [Authorize]
    public ActionResult<Booking> ConfirmBooking([FromQuery] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings.Find(bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        // Don't show bookings from other users.
        if(booking.Customer!.Username != User.Identity!.Name)
            return Problem($"Cannot load booking not for user {User.Identity!.Name}");

        // All due paid off
        booking.Due = 0;
        
        // Save changes in db
        _tourDbContext.Bookings.Update(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={booking.Id}");
    }
    
    /// <summary>
    /// Get current authenticated customer using User.Identity.Name from authentication cookie.
    /// </summary>
    /// <returns>Authenticated customer</returns>
    /// <exception cref="Exception">No user logged in.</exception>
    private Customer CurrentCustomer()
    {
        Customer customer = _tourDbContext.Customers
            .Where(c => c.Username == User.Identity!.Name)
            .FirstOrDefault() ;

        if (customer != null)
            return customer;

        throw new Exception("Invalid User");
    }
    
    /// <summary>
    /// GET handler for all customer bookings.
    /// </summary>
    /// <returns>Bookings page with list of all customer bookings.</returns>
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
    
    /// <summary>
    /// GET handler for booking info page.
    /// </summary>
    /// <param name="bookingId">Id of the booking to examine</param>
    /// <returns>Success: booking info page. Failure: 500 error with message.</returns>
    [HttpGet]
    [Authorize]
    public IActionResult BookingInfo([FromQuery] int bookingId)
    {
        // Load booking, including all relevant fields such as Room, Tour, Hotel.
        Booking? booking = _tourDbContext.Bookings
            .Include(b => b.Customer)
            .Include(b => b.RoomBooking!.Room)
                .ThenInclude(r => r!.Hotel)
            .Include(b => b.TourBooking!.Tour)
            .FirstOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");
        
        // Don't show bookings from other users.
        if(booking.Customer!.Username != User.Identity!.Name)
            return Problem($"Cannot load booking not for user {User.Identity!.Name}");
        
        return View(booking);
    }
    
    /// <summary>
    /// GET handler for booking edit page.
    /// </summary>
    /// <param name="bookingId">Id of booking to edit</param>
    /// <returns>Success: booking edit page. Failure: Error when booking not found.</returns>
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
    
    /// <summary>
    /// POST handler to check if a new booking date is available.
    /// </summary>
    /// <param name="bookingId">Id of booking to check</param>
    /// <param name="from">New start date</param>
    /// <returns>Success: availability of room and tour from that date. Failure: error when booking id invalid.</returns>
    [HttpPost("edit")]
    [Authorize]
    public ActionResult<Booking> CheckEditBooking([FromForm] int bookingId, [FromForm] DateTime from)
    {
        // Extract existing booking from database
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
        
        if (booking.HasRoom()) {
            // Check room availability on that date.
            int days = (booking.RoomBooking!.DateTo - booking.RoomBooking!.DateFrom).Days;
            DateTime newTo = from.AddDays(days);
            
            IEnumerable<Room> rooms = _availabilitySvc
                .RoomsBetweenDates(booking.RoomBooking!.Room!.Hotel, from, newTo)
                .Where(r => r.Id == booking.RoomBooking!.Room!.Id);

            if (rooms.Any()) {
                // Update RoomBooking dates as valid.
                booking.RoomBooking.DateFrom = from;
                booking.RoomBooking.DateTo = newTo;
            }
            else {
                // Set error display message for view
                error = "Room unavailable";
            }
        }

        if (booking.HasTour()) {
            IEnumerable<Tour> tours = _availabilitySvc
                .ToursBetweenDates(from)
                .Where(t => t.Id == booking.TourBooking!.Tour!.Id);

            if (tours.Any()) {
                // Update TourBooking dates as valid.
                booking.TourBooking!.DateFrom = from;
            }
            else {
                // Set error display message for view
                error = "Tour unavailable";
            }
        }

        if (booking.GetDaysUntilStart() <= 14){
            // Add 5% surcharge for changing booking under 14 days from departure.
            booking.Due += (int) (booking.TotalCost * 0.05f);
        }

        // Pass errormsg to the view
        TempData["errormsg"] = error;
        
        return View("EditBooking", booking);
    }
    
    /// <summary>
    /// POST handler to confirm the editing of the booking
    /// </summary>
    /// <param name="bookingId">Id of the booking to update</param>
    /// <param name="from">New start date</param>
    /// <returns>Success: redirect to booking info page. Failure: 500 error with problem</returns>
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

        // Do the same checking as `CheckBookingAvailability` to ensure places have not since been taken.
        
        if (booking.HasRoom()){
            int days = (booking.RoomBooking!.DateTo - booking.RoomBooking!.DateFrom).Days;
            DateTime newTo = from.AddDays(days);
            
            IEnumerable<Room> rooms = _availabilitySvc
                .RoomsBetweenDates(booking.RoomBooking!.Room!.Hotel, from, newTo)
                .Where(r => r.Id == booking.RoomBooking!.Room!.Id);

            if (rooms.Any()) {
                booking.RoomBooking.DateFrom = from;
                booking.RoomBooking.DateTo = newTo;
            }
            else {
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
        
        // Save the new booking.
        _tourDbContext.Bookings.Update(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={bookingId}");
    }
    
    /// <summary>
    /// GET handler to cancel the booking
    /// </summary>
    /// <param name="bookingId">Id of booking to cancel</param>
    /// <returns>Success: page to confirm cancellation. Failure: error when booking id is invalid.</returns>
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
    
    /// <summary>
    /// POST handler to cancel the booking.
    /// </summary>
    /// <param name="bookingId">Id of booking to cancel</param>
    /// <returns>Success: redirect to booking info page. Failure: error when booking id is invalid.</returns>
    [HttpPost("cancel")]
    [Authorize]
    public ActionResult<Booking> ConfirmCancelBooking([FromForm] int bookingId)
    {
        Booking? booking = _tourDbContext.Bookings
            .SingleOrDefault(b => b.Id == bookingId);

        if (booking == null)
            return Problem($"Cannot find booking {bookingId}");

        // Set booking cancelled flag.
        booking.Cancelled = true;
        
        // Save in the database.
        _tourDbContext.Bookings.Update(booking);
        _tourDbContext.SaveChanges();
        
        return Redirect($"/booking?bookingId={bookingId}");
    }
}
