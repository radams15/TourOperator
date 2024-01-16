namespace TourOperator.Controllers;

using System.Collections;
using System.Diagnostics;
using Contexts;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Services;

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
    
    /// <summary>
    /// Home page
    /// </summary>
    /// <returns>View of home page</returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    /// <summary>
    /// Customer info page
    /// </summary>
    /// <returns>View of customer info page</returns>
    [HttpGet("customer")]
    [Authorize]
    public IActionResult Customer()
    {
        return View(CurrentCustomer());
    }
    
    /// <summary>
    /// POST handler for customer update.
    /// </summary>
    /// <param name="customer">Customer information to update</param>
    /// <returns>Success: updated customer info view. Failure: customer info view with validation errors shown</returns>
    [HttpPost("customer")]
    [Authorize]
    public ActionResult Customer([FromForm] Customer customer)
    {
        if (ModelState.IsValid)
        {
            Customer beforeCustomer = CurrentCustomer();
            
            // Only the following fields may be updated.
            beforeCustomer.FullName = customer.FullName;
            beforeCustomer.PhoneNo = customer.PhoneNo;
            beforeCustomer.PassportNo = customer.PassportNo;
            beforeCustomer.Password = customer.Password.Sha256();

            // Update the database.
            _tourDbContext.Customers.Update(beforeCustomer);
            _tourDbContext.SaveChanges();
            
            return View(customer);
        }
        
        // Log validation errors to server log
        foreach (var value in ModelState.Values)
        {
            if(value.Errors.Count > 0)
                foreach(var error in value.Errors)
                    _logger.LogError("Error: {}", error.ErrorMessage);
        }

        return View(customer);
    }
    
    /// <summary>
    /// Hotel search page.
    /// </summary>
    /// <returns>View of hotel search page</returns>
    [HttpGet("Hotels")]
    public IActionResult HotelSearch()
    {
        return View("HotelSearch");
    }
    
    /// <summary>
    /// Tour search page.
    /// </summary>
    /// <returns>View of tour search page</returns>
    [HttpGet("Tours")]
    public IActionResult TourSearch()
    {
        return View("TourSearch");
    }
    
    /// <summary>
    /// POST handler for searching hotels between two dates.
    /// </summary>
    /// <param name="from">Start date in form yyyy-mm-dd</param>
    /// <param name="to">End date in form yyyy-mm-dd</param>
    /// <returns>Hotel search page with available hotels between dates</returns>
    [HttpPost("Hotels")]
    public IActionResult Hotels([FromForm] string from, [FromForm] string to)
    {
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
    
    /// <summary>
    /// POST handler for searching tours between two dates.
    /// </summary>
    /// <param name="from">Start date in form yyyy-mm-dd</param>
    /// <returns>Tour search page with available tours between dates</returns>
    [HttpPost("Tours")]
    public IActionResult Tours([FromForm] string from)
    {
        ViewBag.Message = new Hashtable
        {
            { "fromDate", from }
        };

        IEnumerable<Tour> tours = _availabilitySvc.ToursBetweenDates(
            from.ParseDate()
        );

        return View(tours);
    }

    /// <summary>
    /// Information page about specific hotel on dates, has links to rooms in said hotel.
    /// </summary>
    /// <param name="hotelId">Id of hotel to show</param>
    /// <param name="fromDate">Start date in form yyyy-mm-dd</param>
    /// <param name="toDate">End date in form yyyy-mm-dd</param>
    /// <returns>Success: room list of hotel. Failure: error when hotel id is invalid</returns>
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
    
    /// <summary>
    /// Information page about a tour on a specific date.
    /// </summary>
    /// <param name="tourId">Id of tour to view</param>
    /// <param name="fromDate">Start date in form yyyy-mm-dd</param>
    /// <returns>Success: information about the tour. Failure: error when tour id is invalid.</returns>
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
    /// Checkout page showing the package selected
    /// </summary>
    /// <returns>View showing the selected items</returns>
    [HttpGet("Checkout")]
    [Authorize]
    public IActionResult Checkout()
    {
        Room? room = HttpContext.Session.GetObject<Room>("PackageRoom");
        Tour? tour = HttpContext.Session.GetObject<Tour>("PackageTour");

        RoomBooking? roomBooking = null;
        TourBooking? tourBooking = null;

        if (room != null){
            // Create room booking object if a room is selected.
            
            roomBooking = new RoomBooking{
                Room = room, DateFrom = HttpContext.Session.GetObject<DateTime>("DateFrom"), DateTo = HttpContext.Session.GetObject<DateTime>("RoomDateTo"),
            };
            if (roomBooking.Room != null)
                roomBooking.RoomId = roomBooking.Room!.Id;
        }

        if (tour != null){
            // Create tour booking object if a tour is selected.

            tourBooking = new TourBooking{
                Tour = tour, DateFrom = HttpContext.Session.GetObject<DateTime>("DateFrom"),
            };
            if (tourBooking.Tour != null)
                tourBooking.TourId = tourBooking.Tour.Id;
        }

        // Create the booking object with room and tour booking objects if they exist
        Booking booking = new Booking
        {
            CustomerId = CurrentCustomer().Id,
            RoomBooking = roomBooking,
            TourBooking = tourBooking,
            TotalCost = 0
        };

        if (room != null && roomBooking != null)
        {
            // Add cost of room for n days.
            int days = roomBooking.DateTo.Subtract(roomBooking.DateFrom).Days;
            booking.TotalCost += room.Price * days;
        }

        if (tour != null)
        {
            // Add cost of tour.
            booking.TotalCost += tour.Price;
        }

        if (room != null && tour != null)
        {
            // Package discount specified by the room.
            booking.TotalCost *= 1 - room.PackageDiscount / 100;
        }
        
        return View(booking);
    }

    /// <summary>
    /// View for creating a manager report.
    /// </summary>
    /// <returns>View of ManagerReport creation page</returns>
    [HttpGet("report")]
    [Authorize(Roles = RoleName.Manager)]
    public IActionResult ManagerReport()
    {
        return View();
    }
    
    /// <summary>
    /// POST handler for manager report. Shows all bookings and availability between two dates.
    /// </summary>
    /// <param name="report">Report info inc. start and end dates.</param>
    /// <returns>Report generated between the two dates</returns>
    [HttpPost("report")]
    [Authorize(Roles = RoleName.Manager)]
    public IActionResult GenerateReport([FromForm] Report report)
    {
        // Get all bookings in date range.
        report.Bookings = _tourDbContext.Bookings
            .Include(b => b.RoomBooking)
            .ThenInclude(rb => rb.Room)
            .ThenInclude(r => r.Hotel)
            .Include(b => b.TourBooking)
            .ThenInclude(tb => tb.Tour);

        
        report.DefaultTourAvailability = new Report.Availability<Tour>();
        report.DefaultRoomAvailability = new Report.Availability<Room>();

        // Generate default availability for both rooms and tours.
        // We only calculate this once, and when there are bookings on a day
        // we can subtract the number of hotel/room bookings from the default,
        // empty availability to generate the new availability.
        
        foreach(Room room in _tourDbContext.Rooms.Include(r => r.Hotel)){
            report.DefaultRoomAvailability.Add(
                room,
                room.Spaces
            );
        }
        
        foreach(Tour tour in _tourDbContext.Tours){
            report.DefaultTourAvailability.Add(
                tour,
                tour.Spaces
            );
        }
        
        // Generate the bookings by date they are on for the view.
        
        report.BookingsByDate = new Dictionary<DateTime, List<Booking>>();

        for (var day = report.FromDate; day.Date <= report.ToDate; day = day.AddDays(1)) {
            report.BookingsByDate.Add(day, new List<Booking>());
        }

        foreach(Booking booking in report.Bookings)
        {
            for (var day = booking.StartDate(); day.Date <= booking.EndDate(); day = day.AddDays(1)) {
                if(report.BookingsByDate.Keys.Contains(day))
                    report.BookingsByDate[day].Add(booking);
            }
        }
        
        return View("ManagerReport", report);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
