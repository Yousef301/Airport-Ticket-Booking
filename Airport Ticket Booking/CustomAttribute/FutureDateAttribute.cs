using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.CustomAttribute;

public class FutureDateAttribute : ValidationAttribute
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