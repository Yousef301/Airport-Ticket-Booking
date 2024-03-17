using System.Configuration;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using Airport_Ticket_Booking.UI;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class BookingsRepository
{
    private static readonly string FilePath =
        Helpers.FileHelper.ConcatPaths(Main.ConfigManager.GetValue("DataFiles"), "bookings.csv");

    private static List<Bookings> LoadBookingsList(string filePath)
    {
        List<Bookings> bookings = new List<Bookings>();

        try
        {
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                parser.ReadLine();

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (fields.Length < 3)
                    {
                        Console.WriteLine("Invalid data format: Insufficient fields.");
                        continue;
                    }

                    if (!Int32.TryParse(fields[1], out int flightId))
                    {
                        Console.WriteLine($"Invalid flight ID: {fields[1]}");
                        continue;
                    }

                    if (!Enum.TryParse(fields[2], true, out FlightClass flightClassValue))
                    {
                        Console.WriteLine($"Invalid flight class: {fields[2]}");
                        continue;
                    }

                    User passenger = UserRepository.GetPersonById(fields[0]);
                    if (passenger == null)
                    {
                        Console.WriteLine($"Passenger not found with ID: {fields[0]}");
                        continue;
                    }

                    Flight flight = FlightService.GetFlightById(flightId);
                    if (flight == null)
                    {
                        Console.WriteLine($"Flight not found with ID: {flightId}");
                        continue;
                    }

                    bookings.Add(new Bookings((Passenger)passenger, flight, flightClassValue));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading bookings: {ex.Message}");
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
                    FilePath,
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
            string[] lines = File.ReadAllLines(FilePath);

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

            string[] lines = File.ReadAllLines(FilePath);

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

            File.WriteAllLines(FilePath, updatedLines);

            return bookingRemoved;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public static List<Bookings> GetBookings() => LoadBookingsList(FilePath);
}