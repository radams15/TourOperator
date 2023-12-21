using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TourOperator.Models.Entities;

[PrimaryKey("Username")]
public class Customer
{
    //[StringLength(32, MinimumLength = 4)]
    public string Username { get; set; } = "";
    
    [Required]
    [StringLength(64, MinimumLength = 2)]
    public string? FullName { get; set; } = "";
    
    [StringLength(256, MinimumLength = 4)]
    public string? Password { get; set; } = "";
    
    [Required]
    [RegularExpression(@"\d{11,12}")]
    public string PassportNo { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNo { get; set; }
}