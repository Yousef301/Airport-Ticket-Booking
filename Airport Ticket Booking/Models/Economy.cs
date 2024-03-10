namespace Airport_Ticket_Booking.Models;

public class Economy : IFlight
{
    public int FlightId { get; set; }
    public double Price { get; set; }
    public string DepartureCountry { get; set; } = string.Empty;
    public string DestinationCountry { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; } = string.Empty;
    public string ArrivalAirport { get; set; } = string.Empty;
}