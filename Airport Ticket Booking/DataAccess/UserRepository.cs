using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.UI;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class UserRepository
{
    private static string _filePath =
        Helpers.FileHelper.ConcatPaths(Main.ConfigManager.GetValue("DataFiles"), "users.csv");

    public static User GetPersonById(string id)
    {
        try
        {
            using TextFieldParser parser = new TextFieldParser(_filePath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            parser.ReadLine();

            while (!parser.EndOfData)
            {
                string[] parts = parser.ReadFields();

                if (parts[0].StartsWith("P", StringComparison.OrdinalIgnoreCase) &&
                    parts[0].Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return ParsePassenger(parts);
                }

                if (parts[0].StartsWith("M", StringComparison.OrdinalIgnoreCase) &&
                    parts[0].Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return ParseManager(parts);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("File not found: " + ex.Message);
        }
        return null;
    }

    private static Passenger ParsePassenger(string[] parts)
    {
        return new Passenger
        {
            Id = parts[0],
            FullName = parts[1],
            Email = parts[2],
            PhoneNumber = parts[3],
            DateOfBirth = DateTime.Parse(parts[6]),
            PassportNumber = parts[4],
            Nationality = parts[5]
        };
    }

    private static Manager ParseManager(string[] parts)
    {
        return new Manager
        {
            Id = parts[0],
            FullName = parts[1],
            Email = parts[2],
            PhoneNumber = parts[3],
            DateOfBirth = DateTime.Parse(parts[6]),
            EmployeeId = int.Parse(parts[7])
        };
    }
}