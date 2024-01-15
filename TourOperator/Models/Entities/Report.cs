namespace TourOperator.Models.Entities;

public class Report {
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public IEnumerable<Booking> Bookings { get; set; }
}