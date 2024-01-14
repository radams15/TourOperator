namespace TourOperator.Extensions;

using System.Globalization;

public static class DateTimeExtensions
{
    private static readonly string DateFormat = "yyyy-MM-dd";
    public static string ToShortDate(this DateTime dateTime)
    {
        return dateTime.ToString(DateFormat);
    }
    
    public static DateTime ParseDate(this string str)
    {
        return DateTime.ParseExact(str, DateFormat, CultureInfo.InvariantCulture);
    }
}