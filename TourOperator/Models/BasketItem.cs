namespace TourOperator.Models;

public class BasketItem
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int RoomId { get; set; }
    public int TourId { get; set; }
}