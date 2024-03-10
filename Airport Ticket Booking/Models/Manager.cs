﻿using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.Models;

public class Manager : IUser
{
    private string _fullName;
    private string _email;
    private string _phoneNumber;
    private DateTime _dateOfBirth;
    private int _employeeId;

    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}