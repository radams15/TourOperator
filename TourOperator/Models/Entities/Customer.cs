using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TourOperator.Models.Attributes;

namespace TourOperator.Models.Entities;

public class Customer
{
    public int Id { get; set; }
    
    //[StringLength(32, MinimumLength = 4)]
    [DisplayName("Username")]
    public string Username { get; set; } = "";
    
    [Required]
    [DisplayName("Full Name")]
    [StringLength(64, MinimumLength = 2)]
    public string? FullName { get; set; } = "";
    
    [StringLength(256, MinimumLength = 4)]
    public string? Password { get; set; } = "";
    
    [Required]
    [DisplayName("Passport Number")]
    [PassportNumber]
    public string PassportNo { get; set; }
    
    [Required]
    [DisplayName("Phone Number")]
    [Phone]
    public string PhoneNo { get; set; }
}