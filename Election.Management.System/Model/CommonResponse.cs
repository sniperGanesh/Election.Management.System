using Newtonsoft.Json;

namespace Election.Management.System.Models
{
    public class CommonResponse
    {
        /// <summary>
        /// The actual data from DB
        /// </summary>
        [JsonProperty("data")]
        public object? Data { get; set; }
        /// <summary>
        /// The status code of the Reposne
        /// </summary>

        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        /// <summary>
        /// Common response message
        /// </summary>

        [JsonProperty("message")]
        public string? Message { get; set; }
        /// <summary>
        /// The actual error response
        /// </summary>

        [JsonProperty("errors")]
        public List<string>? Errors { get; set; }
    }
}
