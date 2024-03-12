using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Services;

public class ValidationService
{
    public static bool ValidateObject<T>(T obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (isValid)
        {
        }
        else
        {
            if (typeof(T) == typeof(Flight))
            {
                Flight flight = obj as Flight;
                Console.WriteLine($"{typeof(T).Name} {flight.FlightId} is invalid. Validation errors:");
            }
            else
            {
                Console.WriteLine($"{typeof(T).Name} is invalid. Validation errors:");
            }

            foreach (var validationResult in validationResults)
            {
                Console.WriteLine($"- {validationResult.ErrorMessage}");
            }
        }

        return isValid;
    }
}