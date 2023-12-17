namespace TourOperator.Models.Entities;

public class Hotel
{
    public int Id { get; set; }
    public int OperatorId { get; set; }
    public Operator Operator { get; set; } = null!;
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Room> Rooms { get; set; }
}