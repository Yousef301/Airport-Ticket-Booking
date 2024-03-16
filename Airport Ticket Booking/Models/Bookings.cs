namespace Airport_Ticket_Booking.Models;

public class Bookings
{
    public Passenger Passenger { get; set; }
    public Flight Flight { get; set; }

    public FlightClass FlightClass { get; set; }

    public Bookings(Passenger passenger, Flight flight, FlightClass flightClass)
    {
        FlightClass = flightClass;
        Passenger = passenger;
        Flight = flight;
    }

    public static void ViewBookings(List<Bookings> bookings)
    {
        foreach (var booking in bookings)
        {
            Console.WriteLine(booking.ToString());
        }
    }

    public override string ToString()
    {
        return
            $"Passenger name -> {Passenger.FullName}\nPassenger ID: {Passenger.Id}\nFlight Details:\n{Flight}\nFlight Class -> {FlightClass}\n";
    }
}