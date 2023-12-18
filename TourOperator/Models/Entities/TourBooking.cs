namespace TourOperator.Models.Entities;

public class TourBooking
{
    public int Id { get; set; }
    public int? TourId { get; set; }
    public Tour? Tour { get; set; } = null!;
    
    public DateTime? DateFrom { get; set; } = null!;
    public DateTime? DateTo { get; set; } = null!;
}