using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Services;

public class ValidationService
{
    public static bool IsValidFullName(string fullName)
    {
        return
            !string.IsNullOrEmpty(fullName) &&
            fullName.Length <= 100;
    }

    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        string phonePattern = @"^\+\d{10}$";

        return Regex.IsMatch(phoneNumber, phonePattern);
    }

    public static bool IsValidDateOfBirth(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now;
    }

    public static bool ValidateObject<T>(T obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (isValid)
        {
            Console.WriteLine($"{typeof(T).Name} is valid.");
        }
        else
        {
            Console.WriteLine($"{typeof(T).Name} is invalid. Validation errors:");
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine($"- {validationResult.ErrorMessage}");
            }
        }

        return isValid;
    }
}