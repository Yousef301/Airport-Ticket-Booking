using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.Models;

public class Passenger : User
{
    [Required(ErrorMessage = "Passenger ID is required.")]
    [RegularExpression(@"^[pP]\d+$",
        ErrorMessage = "Passenger ID must start with 'p' or 'P' followed by one or more digits.")]
    public new string Id { get; set; }


    [Required(ErrorMessage = "Passport number is required")]
    public string PassportNumber { get; set; }

    [Required(ErrorMessage = "Nationality is required")]
    public string Nationality { get; set; }

    public Passenger()
    {
    }

    public Passenger(string id, string fullName, string email, string passportNumber, string nationality,
        string phoneNumber, DateTime dateOfBirth) : base(fullName, email, phoneNumber, dateOfBirth)
    {
        Id = id;
        PassportNumber = passportNumber;
        Nationality = nationality;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nPassport number -> {PassportNumber}\nNationality -> {Nationality}";
    }
}