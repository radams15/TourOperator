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

    public List<Room> RoomsBetweenDates(DateTime start, DateTime end)
    {
        /*return _tourDbContext.Rooms.Where(
            
        ).ToList();*/
        return null;
    }
}