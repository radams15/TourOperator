using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models.Entities;

public class Booking
{
    public int Id { get; set; }
    public string? Username { get; set; } = "";
    public Customer? Customer { get; set; }
    
    public int? RoomBookingId { get; set; }
    public RoomBooking? RoomBooking { get; set; }
    
    public int? TourBookingId { get; set; }
    public TourBooking? TourBooking { get; set; }
    
    public DateTime DateBooked { get; set; }
    public int TotalCost { get; set; }
    public bool DepositPaid { get; set; }
    public int Due { get; set; }
}