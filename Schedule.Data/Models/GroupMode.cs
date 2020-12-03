using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Schedule.Data.Models
{
    public enum GroupMode
    {
        [JsonConverter(typeof(StringEnumConverter))]
        FirstGroup,

        [JsonConverter(typeof(StringEnumConverter))]
        SecondGroup,

        [JsonConverter(typeof(StringEnumConverter))]
        ThirdGroup,

        [JsonConverter(typeof(StringEnumConverter))]
        FourthGroup,

        [JsonConverter(typeof(StringEnumConverter))]
        AllGroups
    }
}