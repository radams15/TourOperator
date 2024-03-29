namespace TourOperator.Models.Entities;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Attributes;

public class Customer
{
    public int Id { get; set; }
    
    public int RoleId { get; set; }
    
    public Role Role { get; set; }
    
    [StringLength(32, MinimumLength = 4)]
    [DisplayName("Username")]
    public string Username { get; set; } = "";
    
    [DisplayName("Full Name")]
    [StringLength(64, MinimumLength = 4)]
    public string FullName { get; set; } = "";
    
    [StringLength(64, MinimumLength = 4)]
    public string Password { get; set; } = "";

    [DisplayName("Passport Number")]
    [PassportNumber]
    [StringLength(12)]
    public string PassportNo { get; set; } = "";

    [DisplayName("Phone Number")]
    [Phone]
    public string PhoneNo { get; set; } = "";
}