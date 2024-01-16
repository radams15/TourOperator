namespace TourOperator.Extensions;

using System.Security.Cryptography;
using System.Text;

public static class StringExtensions
{
    /// <summary>
    /// Sha256 method to hash a string.
    /// </summary>
    /// <param name="rawData">String to hash</param>
    /// <returns>Sha256 hash of the string</returns>
    public static string Sha256(this string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
        
            return builder.ToString();
        }
    }
}