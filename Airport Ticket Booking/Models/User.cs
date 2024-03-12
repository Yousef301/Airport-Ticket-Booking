using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.Models;

public class User
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Full name is required")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Invalid full name format")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email address is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\+?[0-9\s]+$", ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.DateTime)]
    public DateTime DateOfBirth { get; set; }

    protected User()
    {
    }

    protected User(string fullName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }

    public override string ToString()
    {
        return
            $"Full Name -> {FullName}\nEmail -> {Email}\nPhone Number -> {PhoneNumber}\nDate of Birth -> {DateOfBirth}";
    }
}