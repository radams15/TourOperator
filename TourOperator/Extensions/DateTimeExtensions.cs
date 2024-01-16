namespace TourOperator.Extensions;

using System.Globalization;

/// <summary>
/// Helper methods extending DateTime to allow converting to/from short date
/// format of yyyy-MM-dd
/// </summary>
public static class DateTimeExtensions
{
    private const string DateFormat = "yyyy-MM-dd";
    public static string ToShortDate(this DateTime dateTime)
    {
        return dateTime.ToString(DateFormat);
    }
    
    public static DateTime ParseDate(this string str)
    {
        return DateTime.ParseExact(str, DateFormat, CultureInfo.InvariantCulture);
    }
}