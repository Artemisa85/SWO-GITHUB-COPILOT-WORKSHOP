using BikeShopAPI.Entities;
using Xunit;

namespace BikeShopAPI.Test.Entities
{
    public class RideLogTest
    {
        [Fact]
        public void Parse_ValidSignature_ReturnsCorrectRideLog()
        {
            // Arrange
            var signature = "17121903-Kitty Hawk, NC-Manteo, NC-WB001";

            // Act
            var rideLog = RideLog.Parse(signature);

            // Assert
            Assert.Equal(new DateTime(1903, 12, 17), rideLog.Date);
            Assert.Equal("Kitty Hawk, NC", rideLog.StartLocation);
            Assert.Equal("Manteo, NC", rideLog.EndLocation);
            Assert.Equal("WB001", rideLog.RouteName);
        }

        [Fact]
        public void Parse_ExampleFromRequirements_ReturnsCorrectRideLog()
        {
            // Arrange - Using the exact example from requirements
            var signature = "17121903-START-END-ROUTE";

            // Act
            var rideLog = RideLog.Parse(signature);

            // Assert
            Assert.Equal(new DateTime(1903, 12, 17), rideLog.Date);
            Assert.Equal("START", rideLog.StartLocation);
            Assert.Equal("END", rideLog.EndLocation);
            Assert.Equal("ROUTE", rideLog.RouteName);
        }

        [Fact]
        public void ToSignature_ValidRideLog_ReturnsCorrectSignature()
        {
            // Arrange
            var rideLog = new RideLog(
                new DateTime(1903, 12, 17),
                "Kitty Hawk, NC",
                "Manteo, NC",
                "WB001"
            );

            // Act
            var signature = rideLog.ToSignature();

            // Assert
            Assert.Equal("17121903-Kitty Hawk, NC-Manteo, NC-WB001", signature);
        }

        [Fact]
        public void TryParse_ValidSignature_ReturnsTrue()
        {
            // Arrange
            var signature = "17121903-Kitty Hawk, NC-Manteo, NC-WB001";

            // Act
            var result = RideLog.TryParse(signature, out var rideLog);

            // Assert
            Assert.True(result);
            Assert.NotNull(rideLog);
            Assert.Equal(new DateTime(1903, 12, 17), rideLog.Date);
        }

        [Fact]
        public void TryParse_InvalidSignature_ReturnsFalse()
        {
            // Arrange
            var signature = "invalid-signature";

            // Act
            var result = RideLog.TryParse(signature, out var rideLog);

            // Assert
            Assert.False(result);
            Assert.Null(rideLog);
        }

        [Fact]
        public void Parse_InvalidFormat_ThrowsArgumentException()
        {
            // Arrange
            var signature = "invalid-format";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => RideLog.Parse(signature));
        }

        [Fact]
        public void Parse_NullSignature_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => RideLog.Parse(null!));
        }

        [Fact]
        public void Parse_InvalidDateFormat_ThrowsArgumentException()
        {
            // Arrange
            var signature = "1712190-START-END-ROUTE"; // Missing one digit

            // Act & Assert
            Assert.Throws<ArgumentException>(() => RideLog.Parse(signature));
        }

        [Fact]
        public void ToString_ValidRideLog_ReturnsFormattedString()
        {
            // Arrange
            var rideLog = new RideLog(
                new DateTime(1903, 12, 17),
                "Kitty Hawk, NC",
                "Manteo, NC",
                "WB001"
            );

            // Act
            var result = rideLog.ToString();

            // Assert
            Assert.Equal("Ride on December 17, 1903 from Kitty Hawk, NC to Manteo, NC via route WB001", result);
        }
    }
}
