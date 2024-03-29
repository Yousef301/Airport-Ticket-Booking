﻿using Airport_Ticket_Booking.Models;

namespace Airport_Ticket_Booking.UI;

public class Menus
{
    public static void LoginMenu()
    {
        Console.Title = "Airport Ticket Booking";

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("╔═══════════════════════════════════════════╗");
        Console.WriteLine("║     Welcome to Airport Ticket Booking     ║");
        Console.WriteLine("╚═══════════════════════════════════════════╝");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Please enter you id to login: ");
        Console.ResetColor();
    }

    public static void PassengerMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Passenger Menu\n\nPlease choose one of the following options:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Book a Flight");
        Console.WriteLine("2. Search for Available Flights");
        Console.WriteLine("3. Manage Bookings");
        Console.WriteLine("4. Exit");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void BookAFlightMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Book a Flight Menu\n\nPlease choose one of the following options:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. View all Flights");
        Console.WriteLine("2. Book a Flight");
        Console.WriteLine("3. Back");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void SearchForFlightMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Search For Flight\n\nPlease choose one of the following options to search by:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Price");
        Console.WriteLine("2. Departure Country");
        Console.WriteLine("3. Destination Country");
        Console.WriteLine("4. Departure Date");
        Console.WriteLine("5. Departure Airport");
        Console.WriteLine("6. Arrival Airport");
        Console.WriteLine("7. Class");
        Console.WriteLine("8. Search");
        Console.WriteLine("9. Clear Filter");
        Console.WriteLine("10. Exit");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void ManageBookingsMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Manage Bookings\n\nPlease choose one of the following options:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Cancel a Booking");
        Console.WriteLine("2. View Personal Bookings");
        Console.WriteLine("3. Exit");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void ManagerMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Passenger Menu\n\nPlease choose one of the following options:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Filter Bookings");
        Console.WriteLine("2. Batch Flight Upload");
        Console.WriteLine("3. Dynamic Model Validation Details");
        Console.WriteLine("4. Exit");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void FilterBookingsMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Filter Bookings\n\nPlease choose one of the following options:");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Flight");
        Console.WriteLine("2. Price");
        Console.WriteLine("3. Departure Country");
        Console.WriteLine("4. Destination Country");
        Console.WriteLine("5. Departure Date");
        Console.WriteLine("6. Departure Airport");
        Console.WriteLine("7. Arrival Airport");
        Console.WriteLine("8. Passenger");
        Console.WriteLine("9. Class");
        Console.WriteLine("10. Filter");
        Console.WriteLine("11. Reset Filter");
        Console.WriteLine("12. Exit");
        Console.WriteLine("========================================");
        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static void BatchFlightUploadMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Batch Flight Upload\n\nPlease enter flights file full path: ");
        Console.ResetColor();
    }

    public static void FlightClasses(List<FlightClass> availableClasses)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Available Flight Classes:\n");

        foreach (FlightClass flightClass in availableClasses)
        {
            Console.WriteLine(GetClassDescription(flightClass));
        }

        Console.Write("Enter your selection: ");
        Console.ResetColor();
    }

    public static string GetClassDescription(FlightClass flightClass)
    {
        switch (flightClass)
        {
            case FlightClass.Economy:
                return "1. Economy";
            case FlightClass.Business:
                return "2. Business";
            case FlightClass.FirstClass:
                return "3. FirstClass";
            default:
                return "Unknown";
        }
    }
}