using System.Text.RegularExpressions;

namespace Airport_Ticket_Booking.Services;

public class ValidationService
{
    public static bool IsValidFullName(string fullName)
    {
        return
            !string.IsNullOrEmpty(fullName) &&
            fullName.Length <= 100;
    }

    public static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email, emailPattern);
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        string phonePattern = @"^\+\d{10}$";

        return Regex.IsMatch(phoneNumber, phonePattern);
    }

    public static bool IsValidDateOfBirth(DateTime dateOfBirth)
    {
        return dateOfBirth <= DateTime.Now;
    }
}