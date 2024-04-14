using Airport_Ticket_Booking.Services;
using FluentAssertions;

namespace AirportTicketBooking.Tests;

public class ValidationServiceTests
{
    [Theory]
    [InlineData(5, 0, 10)]
    [InlineData(0, -5, 5)]
    [InlineData(10, 0, 10)]
    public void IsValidValue_ReturnsTrue_WhenValueIsWithinRange(int value, int min, int max)
    {
        // Act
        var isValid = ValidationService.IsValidValue(value, min, max);

        // Assert
        isValid.Should().BeTrue();
    }
}