using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class BookingsRepository
{
    public static List<Bookings> LoadBookingsList(string filePath)
    {
        List<Bookings> bookings = new List<Bookings>();

        using TextFieldParser parser = new TextFieldParser(filePath);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");

        parser.ReadLine();

        while (!parser.EndOfData)
        {
            string[] fields = parser.ReadFields();

            User passenger = UserRepository.GetPersonById(fields[0],
                "C:\\Users\\shama\\RiderProjects\\Airport Ticket Booking\\Airport Ticket Booking\\Data\\users.csv");


            Int32.TryParse(fields[1], out int flightId);

            Flight flight = FlightService.GetFlightById(flightId);

            bookings.Add(new Bookings((Passenger)passenger, flight));
        }

        return bookings;
    }
}