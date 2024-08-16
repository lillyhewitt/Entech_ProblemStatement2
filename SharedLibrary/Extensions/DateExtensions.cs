namespace Services.Extensions
{
    public static class DateExtensions
    {
        // convert DateTime to DateOnly
        public static DateOnly? ToDateOnly(this DateTime? date)
        {
            // check if date is null
            if (date == null)
            {
                return (DateOnly?)null;
            }

            return DateOnly.FromDateTime(date.Value);
        }

        // convert string to DateOnly
        public static DateOnly? ToDateOnly(this String? date)
        {
            // check if date is null
            if (date == null)
            {
                return (DateOnly?)null;
            }

            if (DateOnly.TryParse(date, out DateOnly result))
            {
                return result;
            };

            throw new FormatException($"String '{date}' was not recognized as a valid DateOnly.");
        }
    }
}