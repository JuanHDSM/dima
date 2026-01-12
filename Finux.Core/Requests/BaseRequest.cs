using System.Text.Json.Serialization;

namespace Finux.Core.Requests
{
    public abstract class BaseRequest
    {
        [JsonIgnore]
        public string UserId { get; set; } = string.Empty;
    }
}