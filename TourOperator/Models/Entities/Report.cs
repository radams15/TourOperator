namespace TourOperator.Models.Entities;

public class Report {
    public class Availability<T> : Dictionary<T, int>
    {
        
    }
    
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public IEnumerable<Booking>? Bookings { get; set; }

    public Availability<Room>? DefaultRoomAvailability { get; set; }
    public Availability<Tour>? DefaultTourAvailability { get; set; }
}