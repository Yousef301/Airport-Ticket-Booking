using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.Models;

public class Passenger : IUser
{
    [Required(ErrorMessage = "Full name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid full name format")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\+?[0-9\s]+$", ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Passport number is required")]
    public string PassportNumber { get; set; }

    [Required(ErrorMessage = "Nationality is required")]
    public string Nationality { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

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