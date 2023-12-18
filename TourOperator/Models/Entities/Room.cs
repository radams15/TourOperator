using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models.Entities;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;
    public string Name { get; set; }
    public int Price { get; set; }
    public int Spaces { get; set; }

    public IEnumerable<RoomBooking>? Bookings { get; set; }
}