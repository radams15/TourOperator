namespace TourOperator.Models.Entities;

public class Booking
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    
    public int? RoomId { get; set; }
    public Room? Room { get; set; } = null!;
    public int? TourId { get; set; }
    public Tour? Tour { get; set; } = null!;
    
    public DateTime DateBooked { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int TotalCost { get; set; }
    public bool DepositPaid { get; set; }
    public int Due { get; set; }
}