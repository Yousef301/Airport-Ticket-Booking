namespace Airport_Ticket_Booking.Models;

public class Bookings
{
    public Passenger Passenger { get; set; }
    public Flight Flight { get; set; }

    public Bookings(Passenger passenger, Flight flight)
    {
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
        return $"Passenger name -> {Passenger.FullName}\nFlight Details:\n{Flight}";
    }
}