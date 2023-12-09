namespace TourOperator.Models;

public class Room
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public int Spaces { get; set; }
}