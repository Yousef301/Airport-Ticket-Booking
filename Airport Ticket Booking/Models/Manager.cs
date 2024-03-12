using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.Models;

public class Manager : User
{
    [Required(ErrorMessage = "Manager ID is required.")]
    [RegularExpression(@"^[mM]\d+$",
        ErrorMessage = "Manager ID must start with 'm' or 'M' followed by one or more digits.")]
    public new string Id { get; set; }


    [Required(ErrorMessage = "Employee ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive integer")]
    public int EmployeeId { get; set; }

    public Manager()
    {
    }

    public Manager(string id, int employeeId, string fullName, string email, string phoneNumber, DateTime dateOfBirth) :
        base(fullName, email, phoneNumber, dateOfBirth)
    {
        Id = id;
        EmployeeId = employeeId;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nEmployee Id -> {EmployeeId}";
    }
}