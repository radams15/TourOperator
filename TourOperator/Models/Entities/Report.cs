namespace TourOperator.Models.Entities;

public class Report {
    public class Availability<T> : Dictionary<T, int>
    {
        /// <summary>
        /// Duplicate the availability dictionary.
        /// This is needed as we keep subtracting from the dictionary
        /// values, but these need to be reset each time.
        /// </summary>
        /// <returns>Duplicate availability dictionary</returns>
        public Availability<T> Duplicate()
        {
            Availability<T> ret = new ();

            foreach(var availability in this){
                ret.Add(availability.Key, availability.Value);
            }
            
            return ret;
        }
    }
    
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }

    public IEnumerable<Booking>? Bookings { get; set; }

    public Dictionary<DateTime, List<Booking>>? BookingsByDate { get; set; }

    public Availability<Room>? DefaultRoomAvailability { get; set; }
    public Availability<Tour>? DefaultTourAvailability { get; set; }
}