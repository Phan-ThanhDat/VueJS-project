using Newtonsoft.Json.Converters;

namespace Aibidia.API.Handlers
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}