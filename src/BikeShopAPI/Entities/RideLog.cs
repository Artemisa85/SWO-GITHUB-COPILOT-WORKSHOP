using System.Globalization;

namespace BikeShopAPI.Entities
{
    public record RideLog(DateTime Date, string StartLocation, string EndLocation, string RouteName)
    {
        /// <summary>
        /// Parses a ride log signature in the format: DDMMYYYY-START-END-ROUTE
        /// Example: 17121903-Kitty Hawk, NC-Manteo, NC-WB001
        /// </summary>
        /// <param name="signature">The ride log signature to parse</param>
        /// <returns>A RideLog record with parsed values</returns>
        /// <exception cref="ArgumentException">Thrown when the signature format is invalid</exception>
        /// <exception cref="ArgumentNullException">Thrown when the signature is null or empty</exception>
        public static RideLog Parse(string signature)
        {
            if (string.IsNullOrWhiteSpace(signature))
                throw new ArgumentNullException(nameof(signature), "Signature cannot be null or empty");

            var parts = signature.Split('-');
            if (parts.Length != 4)
                throw new ArgumentException("Invalid signature format. Expected format: DDMMYYYY-START-END-ROUTE", nameof(signature));

            // Parse date from DDMMYYYY format
            var dateString = parts[0];
            if (dateString.Length != 8)
                throw new ArgumentException("Invalid date format. Expected DDMMYYYY (8 digits)", nameof(signature));

            if (!DateTime.TryParseExact(dateString, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                throw new ArgumentException("Invalid date format. Could not parse date from DDMMYYYY format", nameof(signature));

            var startLocation = parts[1];
            var endLocation = parts[2];
            var routeName = parts[3];

            // Validate that required fields are not empty
            if (string.IsNullOrWhiteSpace(startLocation))
                throw new ArgumentException("Start location cannot be empty", nameof(signature));
            if (string.IsNullOrWhiteSpace(endLocation))
                throw new ArgumentException("End location cannot be empty", nameof(signature));
            if (string.IsNullOrWhiteSpace(routeName))
                throw new ArgumentException("Route name cannot be empty", nameof(signature));

            return new RideLog(date, startLocation, endLocation, routeName);
        }

        /// <summary>
        /// Attempts to parse a ride log signature without throwing exceptions
        /// </summary>
        /// <param name="signature">The ride log signature to parse</param>
        /// <param name="rideLog">The parsed RideLog if successful, null otherwise</param>
        /// <returns>True if parsing was successful, false otherwise</returns>
        public static bool TryParse(string signature, out RideLog? rideLog)
        {
            try
            {
                rideLog = Parse(signature);
                return true;
            }
            catch
            {
                rideLog = null;
                return false;
            }
        }

        /// <summary>
        /// Converts the RideLog back to signature format
        /// </summary>
        /// <returns>The signature string in DDMMYYYY-START-END-ROUTE format</returns>
        public string ToSignature()
        {
            return $"{Date:ddMMyyyy}-{StartLocation}-{EndLocation}-{RouteName}";
        }

        /// <summary>
        /// Returns a formatted string representation of the ride log
        /// </summary>
        /// <returns>A human-readable description of the ride log</returns>
        public override string ToString()
        {
            return $"Ride on {Date:MMMM dd, yyyy} from {StartLocation} to {EndLocation} via route {RouteName}";
        }
    }
}
