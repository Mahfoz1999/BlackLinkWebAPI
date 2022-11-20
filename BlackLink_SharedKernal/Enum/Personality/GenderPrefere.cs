using System.Text.Json.Serialization;

namespace BlackLink_SharedKernal.Enum.Personality
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GenderPrefere
    {
        Women,
        Men,
        Both
    }
}
