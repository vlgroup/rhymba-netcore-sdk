namespace Rhymba.Models.Common
{
    using System.Text.Json.Serialization;

    public class ODataValueWrapper<T>
    {
        [JsonPropertyName("odata.metadata")]
        public string metadata { get; set; } = string.Empty;
        public T? value { get; set; }
    }
}
