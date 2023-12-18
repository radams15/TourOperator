using Microsoft.EntityFrameworkCore;
using TourOperator.Contexts;
using TourOperator.Models.Entities;

namespace TourOperator.Models.Services;

public class AvailabilityService
{
    private readonly TourDbContext _tourDbContext;

    public AvailabilityService(TourDbContext tourDbContext)
    {
        _tourDbContext = tourDbContext;
    }

    private static bool DatesOverlap(DateTime? start1, DateTime? end1, DateTime? start2, DateTime? end2)
    {
        return start1 < end2 && start2 < end1;
    }
    
    public IEnumerable<Room> RoomsBetweenDates(Hotel hotel, DateTime from, DateTime to)
    {
        return hotel.Rooms
            .Where(
                r =>
                        {
                            int count = r.Bookings.Count(b =>
                                DatesOverlap(from, to, b.RoomBooking?.DateFrom, b.RoomBooking?.DateTo)
                            );

                            Console.WriteLine($"{r.Name}: {count} booked, {r.Spaces} spaces.");
                            
                            return count < r.Spaces;
                        }
                );
    }

    public IEnumerable<Hotel> HotelsBetweenDates(DateTime from, DateTime to)
    {
        List<Hotel> allHotels = _tourDbContext.Hotels
            .Include(h => h.Operator)
            .Include(h => h.Rooms)
            .ThenInclude(r => r.Bookings).ToList();
        
        allHotels.ForEach(
            h => h.Rooms = RoomsBetweenDates(h, from, to).ToList()
        );

        return allHotels.Where(h => h.Rooms.Count > 0);
    }
}