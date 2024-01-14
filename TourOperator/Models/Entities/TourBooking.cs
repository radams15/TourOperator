namespace TourOperator.Models.Entities;

public class TourBooking
{
    public int Id { get; set; }
    public int TourId { get; set; }
    public Tour Tour { get; set; }
    
    public DateTime DateFrom { get; set; }
    
    public int BookingId { get; set; }
}