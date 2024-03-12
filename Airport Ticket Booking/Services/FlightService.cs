using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.UI;

namespace Airport_Ticket_Booking.Services;

public class FlightService
{
    public static List<Flight> SearchForFlight(Dictionary<string, object> parameters)
    {
        var filteredFlights = Main.Flights.Where(flight =>
        {
            foreach (var kvp in parameters)
            {
                var property = typeof(Flight).GetProperty(kvp.Key);

                if (property != null)
                {
                    var value = property.GetValue(flight);

                    if (property.PropertyType == typeof(List<FlightClass>))
                    {
                        var flightClassList = value as List<FlightClass>;
                        if (flightClassList == null || !flightClassList.Contains((FlightClass)kvp.Value))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!value.Equals(kvp.Value))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }).ToList();

        return filteredFlights;
    }

    public static Flight GetFlightById(int flightId)
    {
        return Main.Flights.FirstOrDefault(f => f.FlightId == flightId);
    }

    public static Flight GetFlightByIdAndClass(int flightId, FlightClass flightClass)
    {
        return Main.Flights.FirstOrDefault(f => f.FlightId == flightId && f.FlightClass.Contains(flightClass));
    }

    public static void GetFlights()
    {
        Console.WriteLine($"\nAvailable Flights -> {Main.Flights.Count}");
        foreach (var flight in Main.Flights)
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(flight);
        }
    }
}