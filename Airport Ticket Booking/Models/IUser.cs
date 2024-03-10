namespace Airport_Ticket_Booking.Models;

public interface IUser
{
    string FullName { get; set; }
    string Email { get; set; }
    string PhoneNumber { get; set; }
    DateTime DateOfBirth { get; set; }
}