using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.Models;

public class Manager : IUser
{
    private string _fullName;
    private string _email;
    private string _phoneNumber;
    private DateTime _dateOfBirth;
    private int _employeeId;

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

    public int EmployeeId { get; init; }
}