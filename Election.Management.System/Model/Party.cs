using Newtonsoft.Json;

namespace Election.Management.System.Models
{
    public class Party
    {
        /// <summary>
        /// Id of the Party Created
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        ///  Name of the party
        /// </summary>
        [JsonProperty("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// symbol fo the party
        /// </summary>
        [JsonProperty("Symbol")]
        public string? Symbol { get; set; }
    } 
}
