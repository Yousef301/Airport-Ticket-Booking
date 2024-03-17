using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.UI;

namespace Airport_Ticket_Booking.Services;

public class FlightService
{
    public static List<Flight> SearchForFlight(Dictionary<string, object> parameters)
    {
        var filteredFlights = Main.Flights.Values.Where(flight =>
        {
            foreach (var kvp in parameters)
            {
                if (!MatchFlightProperty(flight, kvp.Key, kvp.Value))
                {
                    return false;
                }
            }

            return true;
        }).ToList();

        return filteredFlights;
    }

    private static bool MatchFlightProperty(Flight flight, string propertyName, object expectedValue)
    {
        var property = typeof(Flight).GetProperty(propertyName);

        if (property != null)
        {
            var value = property.GetValue(flight);

            if (property.PropertyType == typeof(List<FlightClass>))
            {
                var flightClassList = value as List<FlightClass>;
                if (flightClassList == null || !flightClassList.Contains((FlightClass)expectedValue))
                {
                    return false;
                }
            }
            else
            {
                if (!value.Equals(expectedValue))
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public static Flight GetFlightById(int flightId)
    {
        return Main.Flights.Values.FirstOrDefault(f => f.FlightId == flightId);
    }

    public static void ListAvailableFlights()
    {
        Console.WriteLine($"Available Flights -> {Main.Flights.Count}");

        foreach (var flight in Main.Flights)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Flight ID: {flight.Key}");
            Console.WriteLine($"Flight Details: {flight.Value}");
        }
    }
}