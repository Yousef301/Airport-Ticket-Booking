using System.ComponentModel.DataAnnotations;
using System.Configuration;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Services;

public class ValidationService
{
    public static bool ValidateObject<T>(T obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (!isValid)
        {
            using StreamWriter writer =
                new StreamWriter(
                    Helpers.FileHelper.ConcatPaths(ConfigurationManager.AppSettings.Get("LogsDirectory"),
                        "LoadedFlightsLog.csv"),
                    true);
            if (typeof(T) == typeof(Flight))
            {
                Flight flight = obj as Flight;
                writer.WriteLine(
                    $"{DateTime.Now}: {typeof(T).Name} {flight.FlightId} is invalid. Validation errors:");
            }
            else
            {
                writer.WriteLine($"{DateTime.Now}: {typeof(T).Name} is invalid. Validation errors:");
            }

            foreach (var validationResult in validationResults)
            {
                writer.WriteLine($"- {validationResult.ErrorMessage}");
            }

            writer.WriteLine();
        }

        return isValid;
    }

    public static bool IsValidValue<T>(T value, T min, T max) where T : IComparable<T>
    {
        return min.CompareTo(value) <= 0 && value.CompareTo(max) <= 0;
    }
}