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
                if (!string.IsNullOrWhiteSpace(dateString))
                {
                    if (DateTime.TryParse(dateString, out var dateTime))
                    {
                        return dateTime;
                    }

                    var ticksString = dateString.Substring("/Date(".Length, dateString.Length - "/Date(".Length - 2);
                    var ticks = long.Parse(ticksString);

                    dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ticks);

                    return dateTime;
                }
            }

            return DateTime.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
