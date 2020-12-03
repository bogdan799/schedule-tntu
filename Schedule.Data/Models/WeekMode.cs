using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Schedule.Data.Models
{
    public enum WeekMode
    {
        [JsonConverter(typeof(StringEnumConverter))]
        FirstWeek,

        [JsonConverter(typeof(StringEnumConverter))]
        SecondWeek,

        [JsonConverter(typeof(StringEnumConverter))]
        AllWeeks
    }
}