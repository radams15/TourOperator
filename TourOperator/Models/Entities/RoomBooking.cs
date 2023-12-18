using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models.Entities;

public class RoomBooking
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    
    public int BookingId { get; set; }
    public Booking Booking { get; set; }
}