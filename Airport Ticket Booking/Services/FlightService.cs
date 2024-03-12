using Airport_Ticket_Booking.DataAccess;
using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.Services;

public class FlightService
{
    public static List<Flight> Flights = FlightsRepository.LoadFlightsFromCsv(
        "C:\\Users\\shama\\RiderProjects\\Airport Ticket Booking\\Airport Ticket Booking\\Data\\flights.csv");


    public static List<Flight> SearchForFlight(Dictionary<string, object> parameters)
    {
        var filteredFlights = Flights.Where(flight =>
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
        return Flights.FirstOrDefault(f => f.FlightId == flightId);
    }
}