using System.Diagnostics.CodeAnalysis;

namespace TourOperator.Models;

public class Booking
{
    public int Id;
    public string Username;
    public int RoomId;
    public int TourId;
    public int PackageId;
    public DateTime DateBooked;
    public DateTime DateFrom;
    public DateTime DateTo;
    public int TotalCost;
    public bool DepositPaid;
    public int Due;
}