using System.ComponentModel.DataAnnotations.Schema;

namespace TourOperator.Models.Entities;

public class Booking
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public RoomBooking? RoomBooking { get; set; }
    public TourBooking? TourBooking { get; set; }
    
    public DateTime DateBooked { get; set; }
    public int TotalCost { get; set; }
    public bool DepositPaid { get; set; }
    public int Due { get; set; }

    public int GetDaysUntilStart()
    {
        return (StartDate() - DateTime.Now).Days;
    }
    public DateTime StartDate()
    {
        DateTime?[] startDates = { RoomBooking?.DateFrom, TourBooking?.DateFrom };

        return startDates
            .Where(d => d != null)
            .OfType<DateTime>()
            .Min();
    }

    public bool IsConfirmed()
    {
        return Due == 0;
    }

    public bool IsForfeit()
    {
        if (IsConfirmed())
            return false;
        
        return GetDaysUntilStart() < 28;
    }
}