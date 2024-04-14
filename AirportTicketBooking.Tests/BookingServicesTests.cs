using System.Reflection;
using Airport_Ticket_Booking.Models;
using Airport_Ticket_Booking.Services;
using FluentAssertions;

namespace AirportTicketBooking.Tests;

public class BookingServicesTests
{
    [Theory]
    [InlineData("Passenger.FullName", "John", true)]
    [InlineData("Passenger.FullName", "Alice", false)]
    [InlineData("Flight.FlightId", 1, true)]
    [InlineData("Flight.FlightId", 2, false)]
    [InlineData("FlightClass", FlightClass.FirstClass, true)]
    [InlineData("FlightClass", FlightClass.Business, false)]
    public void PropertyMatches_ReturnsExpectedResult(string propertyName, object expectedValue, bool expectedResult)
    {
        // Arrange
        var booking = CreateBooking("John", 1, FlightClass.FirstClass);

        var bookingServiceType = typeof(BookingServices);
        var methodInfo = bookingServiceType.GetMethod("PropertyMatches", BindingFlags.NonPublic | BindingFlags.Static);
        var bookingServiceInstance = Activator.CreateInstance(bookingServiceType);

        // Act
        var result = methodInfo.Invoke(bookingServiceInstance, new object[] { booking, propertyName, expectedValue });

        // Assert
        result.Should().Be(expectedResult);
    }

    private Bookings CreateBooking(string passengerName, int flightNumber, FlightClass flightClass)
    {
        return new Bookings(new Passenger { FullName = passengerName }, new Flight { FlightId = flightNumber },
            flightClass);
    }
}