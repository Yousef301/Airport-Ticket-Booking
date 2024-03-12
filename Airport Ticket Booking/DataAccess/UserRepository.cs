using Airport_Ticket_Booking.Models;
using Microsoft.VisualBasic.FileIO;

namespace Airport_Ticket_Booking.DataAccess;

public class UserRepository
{
    public static User GetPersonById(string id, string filePath)
    {
        using TextFieldParser parser = new TextFieldParser(filePath);
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