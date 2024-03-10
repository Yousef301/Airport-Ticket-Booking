using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.CustomAttribute;

namespace Airport_Ticket_Booking.Models;

public class Flight
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "FlightId must be a positive integer.")]
    public int FlightId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "DepartureCountry is required.")]
    public string DepartureCountry { get; set; }

    [Required(ErrorMessage = "DestinationCountry is required.")]
    public string DestinationCountry { get; set; }

    [Required(ErrorMessage = "DepartureDate is required.")]
    [FutureDate(ErrorMessage = "DepartureDate must be in the future.")]
    public DateTime DepartureDate { get; set; }

    [Required(ErrorMessage = "DepartureAirport is required.")]
    public string DepartureAirport { get; set; }

    [Required(ErrorMessage = "ArrivalAirport is required.")]
    public string ArrivalAirport { get; set; }

    [Required(ErrorMessage = "FlightClass is required.")]
    public FlightClass FlightClass { get; set; }

    public override string ToString()
    {
        return
            $"FlightId: {FlightId}\nPrice: {Price}\nDepartureCountry -> {DepartureCountry}, DestinationCountry ->  " +
            $"{DestinationCountry}\nDepartureDate: {DepartureDate}\nDepartureAirport -> {DepartureAirport}," +
            $" ArrivalAirport -> {ArrivalAirport}\nFlightClass: {FlightClass}";
    }
}