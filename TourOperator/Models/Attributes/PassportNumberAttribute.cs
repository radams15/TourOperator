using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TourOperator.Models.Attributes;

public class PassportNumberAttribute : ValidationAttribute
{
    private static readonly string ValidationRegex = @"^(?!^0+$)[a-zA-Z0-9]{3,20}$";
    
    protected override ValidationResult IsValid(object? value, ValidationContext ctx)
    {
        string toValidate = (string) value;

        if (Regex.Match(toValidate, ValidationRegex).Success)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Passport number is invalid");
    }
}