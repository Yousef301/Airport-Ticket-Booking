using System.ComponentModel.DataAnnotations;

namespace Airport_Ticket_Booking.Models;

public interface IFlight
{
    [Required(ErrorMessage = "Name is required.")]
    int FlightId { get; set; }

    double Price { get; set; }
    string DepartureCountry { get; set; }
    string DestinationCountry { get; set; }
    DateTime DepartureDate { get; set; }
    string DepartureAirport { get; set; }
    string ArrivalAirport { get; set; }
}