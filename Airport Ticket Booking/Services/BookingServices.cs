using System.Reflection;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.UI;

namespace Airport_Ticket_Booking.Services;

public class BookingServices
{
    public static List<Bookings> FilterBookings(Dictionary<string, object> filterParams)
    {
        return Main.AllBookings.Where(booking =>
        {
            foreach (var kvp in filterParams)
            {
                if (!PropertyMatches(booking, kvp.Key, kvp.Value))
                    return false;
            }

            return true;
        }).ToList();
    }

    private static bool PropertyMatches(Bookings booking, string propertyName, object expectedValue)
    {
        var properties = propertyName.Split('.');
        object propertyValue = booking;

        foreach (var property in properties)
        {
            PropertyInfo propertyInfo = propertyValue.GetType().GetProperty(property);
            if (propertyInfo == null)
                return false;
            propertyValue = propertyInfo.GetValue(propertyValue);
            if (propertyValue == null)
                return false;
        }

        if (propertyValue is string && expectedValue is string)
        {
            return string.Equals((string)propertyValue, (string)expectedValue, StringComparison.OrdinalIgnoreCase);
        }

        return propertyValue.Equals(expectedValue);
    }
}