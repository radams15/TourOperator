namespace TourOperator.Models.Services;

using Contexts;
using Entities;
using Microsoft.EntityFrameworkCore;

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

    public int RoomCountBetweenDates(Room room, DateTime from, DateTime to)
    {
        return room.Spaces - _tourDbContext.Entry(room)
            .Collection(r => r.Bookings)
            .Query()
            .ToList()
            .Count(b => DatesOverlap(from, to, b.DateFrom,
                b.DateTo));
    }
    
    public IEnumerable<Room> RoomsBetweenDates(Hotel hotel, DateTime from, DateTime to)
    {
        return hotel.Rooms
            .Where(
                r => RoomCountBetweenDates(r, from, to) > 0
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
    
    public int TourSpacesBetweenDates(Tour tour, DateTime from)
    {
        DateTime to = from.AddDays(tour.Length);
        
        return tour.Spaces - _tourDbContext.Entry(tour)
            .Collection(t => t.Bookings)
            .Query()
            .ToList()
            .Count(b => DatesOverlap(from, to, b.DateFrom,
                b.DateFrom.AddDays(b.Tour!.Length)));
    }
    
    public IEnumerable<Tour> ToursBetweenDates(DateTime from)
    {
        List<Tour> allTours = _tourDbContext.Tours
            .Include(t => t.Bookings)
            .ToList();

        return allTours.Where(h => TourSpacesBetweenDates(h, from) > 0);
    }
}