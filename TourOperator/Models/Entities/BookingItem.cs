namespace TourOperator.Models.Entities;

public class BookingItem
{
    public enum BookingType
    {
        Room,
        Tour
    }

    public Type? Type;
    
    public Room? Room { get; set; } = null!;
    public Tour? Tour { get; set; } = null!;
    
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}