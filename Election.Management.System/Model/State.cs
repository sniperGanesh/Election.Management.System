using Newtonsoft.Json;

namespace Election.Management.System.Models
{
    public class State
    {
        /// <summary>
        /// Id of the state
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the State
        /// </summary>
        [JsonProperty("Name")]
        public string? Name { get; set; }

        /// <summary>
        ///  Number of MP Seates Allocated
        /// </summary>
        [JsonProperty("NumberOfMPSeats")]
        public int NumberOfMPSeats { get; set; }
    }
}
