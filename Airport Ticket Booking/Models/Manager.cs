using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.Models;

public class Manager : IUser
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

    [Required(ErrorMessage = "Date of birth is required")]
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Employee ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive integer")]
    public int EmployeeId { get; set; }

    public Manager()
    {
    }

    public Manager(string fullName, string email, string phoneNumber, DateTime dateOfBirth, int employeeId)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
        EmployeeId = employeeId;
    }
}