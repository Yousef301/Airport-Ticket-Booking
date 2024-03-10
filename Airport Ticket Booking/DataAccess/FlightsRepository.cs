using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class FlightsRepository
{
    public static List<Flight> LoadFlightsFromCsv(string filePath)
    {
        List<Flight> flights = new List<Flight>();

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
            FlightClass flightClass;

            if (!int.TryParse(fields[0], out flightId))
                flightId = -1;

            if (!double.TryParse(fields[1], out price))
                price = -1;

            if (!DateTime.TryParse(fields[4], out departureDate))
                departureDate = DateTime.MinValue;

            if (!Enum.TryParse(fields[7], out flightClass))
                flightClass = FlightClass.Economy;

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
                FlightClass = flightClass
            };

            if (ValidationService.ValidateFlight(flight))
            {
                flights.Add(flight);
            }
        }

        return flights;
    }
}