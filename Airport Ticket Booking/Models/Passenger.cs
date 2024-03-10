using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.Models;

public class Passenger : IUser
{
    private string _fullName;
    private string _email;
    private string _phoneNumber;
    private string _passportNumber;
    private string _nationality;
    private DateTime _dateOfBirth;

    public string FullName
    {
        get => _fullName;
        set
        {
            if (!ValidationService.IsValidFullName(value))
            {
                throw new ArgumentException("Invalid full name format");
            }

            _fullName = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (!ValidationService.IsValidEmail(value))
            {
                throw new ArgumentException("Invalid email address format");
            }

            _email = value;
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (!ValidationService.IsValidPhoneNumber(value))
            {
                throw new ArgumentException("Invalid phone number format");
            }

            _phoneNumber = value;
        }
    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (!ValidationService.IsValidDateOfBirth(value))
            {
                throw new ArgumentException("Invalid date of birth");
            }

            _dateOfBirth = value;
        }
    }

    public string PassportNumber
    {
        get => _passportNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid passport number");
            }

            _passportNumber = value;
        }
    }

    public string Nationality
    {
        get => _nationality;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Invalid nationality");
            }

            _nationality = value;
        }
    }

    public Passenger()
    {
    }

    public Passenger(string fullName, string email, string phoneNumber, string passportNumber, string nationality,
        DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        PassportNumber = passportNumber;
        Nationality = nationality;
        DateOfBirth = dateOfBirth;
    }
}