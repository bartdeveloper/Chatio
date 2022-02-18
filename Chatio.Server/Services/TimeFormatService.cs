using System.Globalization;

namespace Chatio.Server.Services
{
    public static class TimeFormatService
    {
        public static string ToPolandFormat(this DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("HH:mm | dd MMMM yyy", new CultureInfo("pl-PL"));
        }
    }
}
