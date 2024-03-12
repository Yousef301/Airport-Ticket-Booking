﻿using Airport_Ticket_Booking.DataAccess;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.UI;

public class Main
{
    public static void Run()
    {
        Menus.LoginMenu();

        bool valid = false;

        do
        {
            var id = Console.ReadLine();

            var user = UserRepository.GetPersonById(id,
                "C:\\Users\\shama\\RiderProjects\\Airport Ticket Booking\\Airport Ticket Booking\\Data\\users.csv");

            if (user is not null)
            {
                if (user.GetType() == typeof(Passenger))
                {
                    Passenger passenger = (Passenger)user;
                    valid = true;

                    var option = "0";

                    do
                    {
                        Console.Clear();
                        Menus.PassengerMenu();

                        option = Console.ReadLine();

                        if (option == "" || !int.TryParse(option, out _))
                        {
                            option = "0";
                        }

                        switch (option)
                        {
                            case "1":
                                Console.Clear();

                                var selection = "0";

                                do
                                {
                                    Menus.BookAFlightMenu();
                                    selection = Console.ReadLine();

                                    if (selection == "" || !int.TryParse(selection, out _))
                                    {
                                        selection = "0";
                                    }

                                    switch (selection)
                                    {
                                        case "1":
                                            FlightService.GetFlights();
                                            break;
                                        case "2":
                                            break;
                                        case "3":
                                            Console.Write("Enter flight id: ");
                                            var flightId = Console.ReadLine();

                                            if (flightId == "" || !int.TryParse(flightId, out _))
                                            {
                                                flightId = "-1";
                                            }

                                            Flight flight = FlightService.GetFlightById(int.Parse(flightId));

                                            if (flight is not null)
                                            {
                                                BookingsRepository.InsertBooking(passenger.Id, int.Parse(flightId));
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Couldn't book Flight {flightId}");
                                            }

                                            break;
                                        case "4":
                                            break;
                                        default:
                                            Log.InvalidInputMessage(
                                                "Invalid input. Please enter 1, 2, 3 or 4 to select an option from the menu.");
                                            break;
                                    }

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Enter your selection: ");
                                    Console.ResetColor();
                                } while (Int32.Parse(selection) != 4);

                                break;
                            case "2":
                                Console.Clear();
                                var selection1 = 0;
                                Dictionary<string, object> filter = new Dictionary<string, object>();

                                do
                                {
                                    Menus.SearchForFlightMenu();
                                    int.TryParse(Console.ReadLine(), out selection1);

                                    switch (selection1)
                                    {
                                        case 1:
                                            var price = EnterValue("Price");

                                            if (double.TryParse(price, out double pri))
                                            {
                                                if (!filter.ContainsKey("Price"))
                                                {
                                                    filter.Add("Price", pri);
                                                }
                                                else
                                                {
                                                    filter["Price"] = pri;
                                                }
                                            }

                                            break;
                                        case 2:
                                            UpdateFilter("DepartureCountry", "Departure Country", filter);
                                            break;
                                        case 3:
                                            UpdateFilter("DestinationCountry", "Destination Country", filter);
                                            break;
                                        case 4:
                                            var departureDate = EnterValue("Departure Date (yyyy-MM-dd)");

                                            if (DateTime.TryParseExact(departureDate, "yyyy-MM-dd", null,
                                                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                                            {
                                                if (!filter.ContainsKey("DepartureDate"))
                                                {
                                                    filter.Add("DepartureDate", parsedDate);
                                                }
                                                else
                                                {
                                                    filter["DepartureDate"] = parsedDate;
                                                }
                                            }

                                            break;
                                        case 5:
                                            UpdateFilter("DepartureAirport", "Departure Airport", filter);
                                            break;
                                        case 6:
                                            UpdateFilter("ArrivalAirport", "Arrival Airport", filter);
                                            break;
                                        case 7:
                                            var flightClass = EnterValue("Flight Class");

                                            if (Enum.TryParse(flightClass, true, out FlightClass flightClassValue))
                                            {
                                                if (!filter.ContainsKey("FlightClass"))
                                                {
                                                    filter.Add("FlightClass", flightClassValue);
                                                }
                                                else
                                                {
                                                    filter["FlightClass"] = flightClassValue;
                                                }
                                            }

                                            break;
                                        case 8:
                                            ViewFilteredFlights(filter);
                                            break;
                                        case 9:
                                            break;
                                        default:
                                            Log.InvalidInputMessage(
                                                "Invalid input. Please enter a number between 1 and 9.");
                                            break;
                                    }

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Enter your selection: ");
                                    Console.ResetColor();
                                } while (selection1 != 9);


                                break;
                            case "3":
                                Console.Clear();

                                var selection2 = "0";

                                do
                                {
                                    Menus.ManageBookingsMenu();
                                    selection2 = Console.ReadLine();

                                    if (selection2 == "" || !int.TryParse(selection2, out _))
                                    {
                                        selection2 = "0";
                                    }

                                    switch (selection2)
                                    {
                                        case "1":
                                            Console.Clear();
                                            Console.Write("Enter flight id: ");
                                            var flightId = Console.ReadLine();

                                            if (flightId == "" || !int.TryParse(flightId, out _))
                                            {
                                                flightId = "-1";
                                            }

                                            var removed =
                                                BookingsRepository.RemoveBooking(passenger.Id, int.Parse(flightId));

                                            if (removed)
                                            {
                                                Console.WriteLine("Booking removed successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Couldn't remove the booking.");
                                            }

                                            break;
                                        case "2":
                                            Console.Clear();
                                            List<Bookings> bookings =
                                                BookingsRepository.GetBookingsForPassenger(passenger);
                                            Bookings.ViewBookings(bookings);
                                            break;
                                        case "3":
                                            break;
                                        default:
                                            Log.InvalidInputMessage(
                                                "Invalid input. Please enter 1, 2, or 3 to select an option from the menu.");
                                            break;
                                    }

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Enter your selection: ");
                                    Console.ResetColor();
                                } while (Int32.Parse(selection2) != 3);

                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Log.InvalidInputMessage(
                                    "Invalid input. Please enter 1, 2, 3, or 4 to select an option from the menu.");
                                break;
                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Enter your selection: ");
                        Console.ResetColor();
                    } while (Int32.Parse(option) != 4);
                }
                else if (user.GetType() == typeof(Manager))
                {
                    Manager manager = (Manager)user;
                    valid = true;
                    Console.Clear();
                    Menus.ManagerMenu();
                }
            }
            else Console.WriteLine("Invalid Id");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Please enter you id to login: ");
            Console.ResetColor();
        } while (!valid);
    }

    static void UpdateFilter(string key, string prompt, Dictionary<string, object> filter)
    {
        var value = EnterValue(prompt);
        if (!string.IsNullOrEmpty(value))
        {
            if (!filter.ContainsKey(key))
            {
                filter.Add(key, value);
            }
            else
            {
                filter[key] = value;
            }
        }
    }

    static void ViewFilteredFlights(Dictionary<string, object> filter)
    {
        List<Flight> flights = FlightService.SearchForFlight(filter);
        Flight.ViewFlights(flights);
    }

    static string EnterValue(string prompt)
    {
        Console.Write($"Enter {prompt}: ");
        return Console.ReadLine();
    }
}