using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Services
{
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
                        "C:\\Users\\shama\\RiderProjects\\Airport Ticket Booking\\Airport Ticket Booking\\Logs\\LoadedFlightsLog.txt",
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
    }
}