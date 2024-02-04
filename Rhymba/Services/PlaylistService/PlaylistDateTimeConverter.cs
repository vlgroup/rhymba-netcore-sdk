namespace Rhymba.Services.Playlist
{
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class PlaylistDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var dateString = reader.GetString() ?? string.Empty;
                // Extract ticks from the string
                var ticksString = dateString.Substring("/Date(".Length, dateString.Length - "/Date(".Length - 2);
                var ticks = long.Parse(ticksString);

                // Convert ticks to DateTime
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ticks);

                return dateTime;
            }

            throw new JsonException($"Unable to parse the date.");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
