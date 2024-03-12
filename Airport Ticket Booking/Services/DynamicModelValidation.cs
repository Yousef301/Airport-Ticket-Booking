using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Airport_Ticket_Booking.CustomAttribute;

namespace Airport_Ticket_Booking.Services;

public class DynamicModelValidation
{
    public static void GenerateValidationConstraints(Type type)
    {
        PropertyInfo[] properties = type.GetProperties();

        Console.ForegroundColor = ConsoleColor.Green;
        foreach (PropertyInfo property in properties)
        {
            Console.WriteLine("========================================");
            Console.WriteLine(
                $"{property.Name}:\nType: {GetPropertyType(property)}\nConstraint: {GetValidationConstraints(property)}");
            Console.WriteLine("========================================");
        }

        Console.ResetColor();
    }

    private static string GetPropertyType(PropertyInfo property)
    {
        return property.PropertyType.Name;
    }

    private static string GetValidationConstraints(PropertyInfo property)
    {
        var validationAttributes = property.GetCustomAttributes<ValidationAttribute>(true);
        return string.Join("\n", validationAttributes.Select(GetConstraintDescription));
    }

    private static string GetConstraintDescription(ValidationAttribute attribute)
    {
        if (attribute is RequiredAttribute)
            return "Required";
        else if (attribute is RangeAttribute rangeAttribute)
            return $"Range ({rangeAttribute.Minimum}, {rangeAttribute.Maximum})";
        else if (attribute is FutureDateAttributes)
            return "Must be in the future";
        else if (attribute is FlightClassValidatorAttribute)
            return "Flight Class cannot be Unknown";
        else
            return attribute.GetType().Name;
    }
}