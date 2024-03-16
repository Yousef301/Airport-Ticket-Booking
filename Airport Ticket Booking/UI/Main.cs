using Airport_Ticket_Booking.DataAccess;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;

namespace Airport_Ticket_Booking.UI;

public class Main
{
    public static List<Flight> Flights = FlightsRepository.GetFlights("");

    public static List<Bookings> AllBookings = BookingsRepository.GetBookings();

    public static void Run()
    {
        Menus.LoginMenu();

        bool valid = false;

        do
        {
            var id = Console.ReadLine();

            var user = UserRepository.GetPersonById(id);

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
                                            Console.Write("Enter flight id: ");
                                            var flightId = Console.ReadLine();

                                            if (flightId == "" || !int.TryParse(flightId, out _))
                                            {
                                                flightId = "-1";
                                            }

                                            int fClass;
                                            bool isValidChoice;

                                            Console.Clear();
                                            var flightC = FlightService.GetFlightById(int.Parse(flightId));
                                            List<FlightClass> flightClasses = new List<FlightClass>();

                                            if (flightC is not null)
                                            {
                                                flightClasses = flightC.FlightClass;

                                                Menus.FlightClasses(flightClasses);

                                                var numericValues = flightClasses.Select(fc => (int)fc).ToList();

                                                do
                                                {
                                                    string input = Console.ReadLine();
                                                    isValidChoice = int.TryParse(input, out fClass) &&
                                                                    numericValues.Contains(fClass - 1);

                                                    if (!isValidChoice)
                                                    {
                                                        Console.WriteLine(
                                                            "Invalid selection. Please enter from the above options.");
                                                        Console.Write("Enter your selection: ");
                                                    }
                                                } while (!isValidChoice);


                                                BookingsRepository.InsertBooking(passenger.Id, int.Parse(flightId),
                                                    (FlightClass)(fClass - 1));
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Flight {flightId} is not available.");
                                            }

                                            break;
                                        case "3":
                                            break;
                                        default:
                                            Log.InvalidInputMessage(
                                                "Invalid input. Please enter 1, 2 or 3 to select an option from the menu.");
                                            break;
                                    }

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.Write("Enter your selection: ");
                                    Console.ResetColor();
                                } while (Int32.Parse(selection) != 3);

                                break;
                            case "2":
                                SearchForFlight();
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
                                                Console.WriteLine($"Book {flightId} cancelled successfully.");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Book {flightId} cancelled unsuccessfully.");
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
                    } while (Int32.Parse(option) != 5);
                }
                else if (user.GetType() == typeof(Manager))
                {
                    Manager manager = (Manager)user;
                    valid = true;


                    var option = "0";

                    do
                    {
                        Menus.ManagerMenu();

                        option = Console.ReadLine();

                        if (option == "" || !int.TryParse(option, out _))
                        {
                            option = "0";
                        }

                        switch (option)
                        {
                            case "1":
                                FilterBookings();
                                break;
                            case "2":
                                Menus.BatchFlightUploadMenu();
                                var filePath = Console.ReadLine();

                                if (File.Exists(filePath))
                                {
                                    var flights = FlightsRepository.GetFlights(filePath);
                                    Console.WriteLine(
                                        @"Check 'C:\Users\shama\RiderProjects\Airport Ticket Booking\Airport Ticket Booking\Logs\LoadedFlightsLog.txt' for validation details.");

                                    FlightsRepository.AddFlightsToTheFlightsFile(flights);
                                }
                                else Console.WriteLine("File does not exist or the path is invalid.");

                                break;
                            case "3":
                                DynamicModelValidation.GenerateValidationConstraints(typeof(Flight));
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Log.InvalidInputMessage(
                                    "Invalid input. Please enter 1, 2, 3 or 4 to select an option from the menu.");
                                break;
                        }
                    } while (Int32.Parse(option) != 4);
                }
            }
            else Console.WriteLine("Invalid Id");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Please enter you id to login: ");
            Console.ResetColor();
        } while (!valid);
    }

    private static void SearchForFlight()
    {
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
                    UpdateFilter<double>("Price", "Price", filter);
                    break;
                case 2:
                    UpdateFilter<string>("DepartureCountry", "Departure Country", filter);
                    break;
                case 3:
                    UpdateFilter<string>("DestinationCountry", "Destination Country", filter);
                    break;
                case 4:
                    UpdateFilter<DateTime>("DepartureDate", "Departure Date (yyyy-MM-dd HH:mm:ss)", filter);
                    break;
                case 5:
                    UpdateFilter<string>("DepartureAirport", "Departure Airport", filter);
                    break;
                case 6:
                    UpdateFilter<string>("ArrivalAirport", "Arrival Airport", filter);
                    break;
                case 7:
                    UpdateFilter<Enum>("FlightClass", "Flight Class", filter);
                    break;
                case 8:
                    ViewFilteredFlights(filter);
                    break;
                case 9:
                    filter = new Dictionary<string, object>();
                    break;
                case 10:
                    break;
                default:
                    Log.InvalidInputMessage(
                        "Invalid input. Please enter a number between 1 and 10.");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your selection: ");
            Console.ResetColor();
        } while (selection1 != 10);
    }

    private static void FilterBookings()
    {
        Console.Clear();
        var selection1 = 0;
        Dictionary<string, object> filter = new Dictionary<string, object>();

        do
        {
            Menus.FilterBookingsMenu();
            int.TryParse(Console.ReadLine(), out selection1);

            switch (selection1)
            {
                case 1:
                    UpdateFilter<double>("Flight.FlightId", "Flight ID", filter);
                    break;
                case 2:
                    UpdateFilter<double>("Flight.Price", "Price", filter);
                    break;
                case 3:
                    UpdateFilter<string>("Flight.DepartureCountry", "Departure Country", filter);
                    break;
                case 4:
                    UpdateFilter<string>("Flight.DestinationCountry", "Destination Country", filter);
                    break;
                case 5:
                    UpdateFilter<DateTime>("Flight.DepartureDate", "Departure Date (yyyy-MM-dd HH:mm:ss)", filter);
                    break;
                case 6:
                    UpdateFilter<string>("Flight.DepartureAirport", "Departure Airport", filter);
                    break;
                case 7:
                    UpdateFilter<string>("Flight.ArrivalAirport", "Arrival Airport", filter);
                    break;
                case 8:
                    UpdateFilter<string>("Passenger.Id", "Passenger Id", filter);
                    break;
                case 9:
                    UpdateFilter<Enum>("FlightClass", "Flight Class", filter);
                    break;
                case 10:
                    ViewFilteredBookings(filter);
                    break;
                case 11:
                    filter = new Dictionary<string, object>();
                    break;
                case 12:
                    break;
                default:
                    Log.InvalidInputMessage(
                        "Invalid input. Please enter a number between 1 and 12.");
                    break;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Enter your selection: ");
            Console.ResetColor();
        } while (selection1 != 12);
    }

    private static void UpdateFilter<T>(string key, string prompt, Dictionary<string, object> filter)
    {
        var value = EnterValue(prompt);
        if (!string.IsNullOrEmpty(value))
        {
            if (typeof(T) == typeof(string))
            {
                filter[key] = value;
            }
            else if (typeof(T) == typeof(double) && double.TryParse(value, out double price))
            {
                if (ValidationService.IsValidValue(price, 0, 1000))
                {
                    filter[key] = price;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{price:C} is invalid price. Please try again.");
                    Console.ResetColor();
                }
            }
            else if (typeof(T) == typeof(int) && int.TryParse(value, out int id))
            {
                if (ValidationService.IsValidValue(id, 0, 1000))
                {
                    filter[key] = id;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"{id} is invalid flight id. Please try again.");
                    Console.ResetColor();
                }
            }
            else if (typeof(T) == typeof(DateTime) && DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", null,
                         System.Globalization.DateTimeStyles.None, out DateTime parsedDateTime))
            {
                filter[key] = parsedDateTime;
            }
            else if (typeof(T) == typeof(Enum) && Enum.TryParse(value, true, out FlightClass flightClassValue))
            {
                filter[key] = flightClassValue;
            }
        }
    }

    private static string EnterValue(string prompt)
    {
        Console.Write($"Enter {prompt}: ");
        return Console.ReadLine();
    }

    private static void ViewFilteredFlights(Dictionary<string, object> filter)
    {
        var flights = FlightService.SearchForFlight(filter);
        Flight.ViewFlights(flights);
    }

    private static void ViewFilteredBookings(Dictionary<string, object> filter)
    {
        var bookings = BookingServices.FilterBookings(filter);
        Bookings.ViewBookings(bookings);
    }
}