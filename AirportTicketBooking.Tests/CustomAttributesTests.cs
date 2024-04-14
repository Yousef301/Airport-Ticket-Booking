using System.ComponentModel.DataAnnotations;
using Airport_Ticket_Booking.CustomAttribute;
using Airport_Ticket_Booking.Models;
using FluentAssertions;

namespace AirportTicketBooking.Tests;

public class CustomAttributesTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(-1, false)]
    public void FutureDateValidationAttribute_ValidatesFutureDatesCorrectly(int daysToAdd, bool expectedIsValid)
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(daysToAdd);
        var attribute = new FutureDateValidationAttribute();

        // Act
        var isValid = attribute.IsValid(futureDate);

        // Assert
        isValid.Should().Be(expectedIsValid);
    }

    [Fact]
    public void FlightClassValidationAttribute_ValidatesFlightClassesCorrectly()
    {
        // Arrange
        var validFlightClasses = new List<FlightClass> { FlightClass.Economy, FlightClass.Business };
        var invalidFlightClasses = new List<FlightClass> { FlightClass.Economy, FlightClass.Unknown };
        var attribute = new FlightClassValidationAttribute();

        // Act
        var validationResultForValid =
            attribute.GetValidationResult(validFlightClasses, new ValidationContext(validFlightClasses));
        var validationResultForInvalid =
            attribute.GetValidationResult(invalidFlightClasses, new ValidationContext(invalidFlightClasses));

        // Assert
        validationResultForValid.Should().BeNull("because the list contains only valid flight classes");
        validationResultForInvalid.Should().NotBeNull("because the list contains an invalid flight class");
    }
}