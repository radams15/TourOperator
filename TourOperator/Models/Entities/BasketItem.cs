using System.ComponentModel.DataAnnotations;

namespace TourOperator.Models.Entities;

public class BasketItem
{
    public int Id { get; set; }
    
    [Required]
    public string? Username { get; set; } = "";
    
    public int? RoomId { get; set; }
    public Room? Room { get; set; } = null!;

    public int? TourId { get; set; }
    public Tour? Tour { get; set; } = null!;
}