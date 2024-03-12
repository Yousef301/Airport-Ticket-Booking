using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.CustomAttribute;

public class FutureDateAttribues : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }

        return false;
    }
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class FlightClassValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is List<FlightClass> flightClasses)
        {
            if (flightClasses.Contains(FlightClass.Unknown))
            {
                return new ValidationResult(ErrorMessage);
            }
        }

        return ValidationResult.Success;
    }
}