using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.CustomAttribute;

namespace Airport_Ticket_Booking.Models;

public class Flight
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Flight Id must be a positive integer.")]
    public int FlightId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Departure Country is required.")]
    public string DepartureCountry { get; set; }

    [Required(ErrorMessage = "Destination Country is required.")]
    public string DestinationCountry { get; set; }

    [Required(ErrorMessage = "Departure Date is required.")]
    [FutureDate(ErrorMessage = "Departure Date must be in the future.")]
    public DateTime DepartureDate { get; set; }

    [Required(ErrorMessage = "Departure Airport is required.")]
    public string DepartureAirport { get; set; }

    [Required(ErrorMessage = "Arrival Airport is required.")]
    public string ArrivalAirport { get; set; }

    [Required(ErrorMessage = "Flight Class is required.")]
    public FlightClass FlightClass { get; set; }

    public override string ToString()
    {
        return
            $"Flight Id: {FlightId}\nPrice: {Price}\nDeparture Country -> {DepartureCountry}, Destination Country ->  " +
            $"{DestinationCountry}\nDeparture Date: {DepartureDate}\nDeparture Airport -> {DepartureAirport}," +
            $" Arrival Airport -> {ArrivalAirport}\nFlight Class: {FlightClass}";
    }
}