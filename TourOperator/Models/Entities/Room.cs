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

    public IEnumerable<Booking> Bookings { get; set; } = null!;

    [NotMapped]
    public DateTime FromDate { get; set; }
    [NotMapped]
    public DateTime ToDate { get; set; }
}