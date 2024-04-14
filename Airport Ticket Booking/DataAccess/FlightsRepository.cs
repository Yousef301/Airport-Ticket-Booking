using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking.UI;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class FlightsRepository
{
    private static readonly string FilePath =
        Helpers.FileHelper.ConcatPaths(Main.ConfigManager.GetValue("DataFiles"), "flights.csv");

    private static Dictionary<int, Flight> LoadFlightsFromCsv(string filePath)
    {
        Dictionary<int, Flight> flights = new Dictionary<int, Flight>();

        try
        {
            using TextFieldParser parser = new TextFieldParser(filePath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            parser.ReadLine();

            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();

                int flightId;
                double price;
                DateTime departureDate;
                List<FlightClass> flightClasses = ParseFlightClasses(fields[7]);

                if (!int.TryParse(fields[0], out flightId))
                    flightId = -1;

                if (!double.TryParse(fields[1], out price))
                    price = -1;

                if (!DateTime.TryParse(fields[4], out departureDate))
                    departureDate = DateTime.MinValue;

                if (flightClasses.Count == 0)
                    flightClasses.Add(FlightClass.Unknown);

                string departureCountry = fields[2];
                string destinationCountry = fields[3];
                string departureAirport = fields[5];
                string arrivalAirport = fields[6];

                Flight flight = new Flight
                {
                    FlightId = flightId,
                    Price = price,
                    DepartureCountry = departureCountry,
                    DestinationCountry = destinationCountry,
                    DepartureDate = departureDate,
                    DepartureAirport = departureAirport,
                    ArrivalAirport = arrivalAirport,
                    FlightClass = flightClasses
                };

                if (ValidationService.ValidateObject(flight))
                {
                    flights.Add(flightId, flight);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File not found: " + ex.Message);
        }

        return flights;
    }


    private static List<FlightClass> ParseFlightClasses(string classString)
    {
        string[] classNames = classString
            .Replace("[", "")
            .Replace("]", "")
            .Split('-');

        List<FlightClass> flightClasses = new List<FlightClass>();

        foreach (var className in classNames)
        {
            if (Enum.TryParse(className.Trim(), out FlightClass flightClass))
            {
                flightClasses.Add(flightClass);
            }
        }

        return flightClasses;
    }

    public static void AddFlightsToTheFlightsFile(Dictionary<int, Flight> flights)
    {
        using StreamWriter writer = File.AppendText(FilePath);

        foreach (var flight in flights.Values)
        {
            var classes = "[" + string.Join("-", flight.FlightClass) + "]";

            string newRecord =
                $"{flight.FlightId},{flight.Price},{flight.DepartureCountry},{flight.DestinationCountry}," +
                $"{flight.DepartureDate},{flight.DepartureAirport},{flight.ArrivalAirport},{classes}";

            writer.WriteLine(newRecord);
        }
    }

    public static Dictionary<int, Flight> GetFlights(string filePath)
    {
        var path = filePath.Equals("")
            ? FilePath
            : filePath;
        return LoadFlightsFromCsv(path);
    }
}