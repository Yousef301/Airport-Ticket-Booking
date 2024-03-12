using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.CustomAttribute;

public class CustomAttributes : ValidationAttribute
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
public class NotEqualToAttribute : ValidationAttribute
{
    private readonly object _comparisonValue;

    public NotEqualToAttribute(object comparisonValue)
    {
        _comparisonValue = comparisonValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null && value.Equals(_comparisonValue))
        {
            return new ValidationResult(ErrorMessage ?? $"Value cannot be {_comparisonValue.ToString()}.");
        }

        return ValidationResult.Success;
    }
}