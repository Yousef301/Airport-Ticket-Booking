using System.Configuration;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class BookingsRepository
{
    private static string _filePath =
        Helpers.FileHelper.ConcatPaths(ConfigurationManager.AppSettings.Get("DataFiles"), "bookings.csv");

    private static List<Bookings> LoadBookingsList(string filePath)
    {
        List<Bookings> bookings = new List<Bookings>();

        using TextFieldParser parser = new TextFieldParser(filePath);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");

        parser.ReadLine();

        while (!parser.EndOfData)
        {
            string[] fields = parser.ReadFields();

            User passenger = UserRepository.GetPersonById(fields[0]);


            Int32.TryParse(fields[1], out int flightId);
            Enum.TryParse(fields[2], true, out FlightClass flightClassValue);

            Flight flight = FlightService.GetFlightById(flightId);

            bookings.Add(new Bookings((Passenger)passenger, flight, flightClassValue));
        }

        return bookings;
    }

    public static void InsertBooking(string pid, int fid, FlightClass flightClass)
    {
        Console.WriteLine();

        try
        {
            string dataLine = $"{pid},{fid}, {flightClass}";

            using StreamWriter writer =
                new StreamWriter(
                    _filePath,
                    true);
            writer.WriteLine(dataLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    public static List<Bookings> GetBookingsForPassenger(Passenger passenger)
    {
        List<Bookings> bookings = new List<Bookings>();

        try
        {
            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields.Length >= 3)
                {
                    string pid = fields[0];
                    int fid;
                    if (int.TryParse(fields[1], out fid) &&
                        Enum.TryParse(fields[2], true, out FlightClass flightClassValue))
                    {
                        if (pid.Equals(passenger.Id))
                        {
                            Flight flight = FlightService.GetFlightById(fid);
                            bookings.Add(new Bookings(passenger, flight, flightClassValue));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return bookings;
    }

    public static bool RemoveBooking(string targetPid, int targetFid)
    {
        try
        {
            List<string> updatedLines = new List<string>();
            bool bookingRemoved = false;

            string[] lines = File.ReadAllLines(_filePath);

            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields.Length >= 2)
                {
                    string pid = fields[0];
                    int fid;
                    if (int.TryParse(fields[1], out fid))
                    {
                        if (pid.Equals(targetPid) && fid == targetFid)
                        {
                            bookingRemoved = true;
                            continue;
                        }
                    }
                }

                updatedLines.Add(line);
            }

            File.WriteAllLines(_filePath, updatedLines);

            return bookingRemoved;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public static List<Bookings> GetBookings() => LoadBookingsList(_filePath);
}