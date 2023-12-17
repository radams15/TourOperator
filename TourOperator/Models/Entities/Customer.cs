using Microsoft.EntityFrameworkCore;

namespace TourOperator.Models.Entities;

[PrimaryKey("Username")]
public class Customer
{
    public string Username { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Password { get; set; } = "";
    public int PassportNo { get; set; }
    public int PhoneNo { get; set; }
}