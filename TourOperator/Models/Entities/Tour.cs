namespace TourOperator.Models.Entities;

public class Tour
{
    public int Id { get; set; }
    public int Spaces { get; set; }
    public int Price { get; set; }
    public int Length { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public IEnumerable<Booking> Bookings { get; set; } = null!;
}