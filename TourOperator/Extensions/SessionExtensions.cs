namespace TourOperator.Extensions;

using System.Text.Json;

/// <summary>
/// Extension to ISession to allow serialising and deserialising
/// to JSON.
/// </summary>
public static class SessionExtensions
{
    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }
    
    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
    }
}