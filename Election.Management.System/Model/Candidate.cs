using Newtonsoft.Json;

namespace Election.Management.System.Models
{
    public class Candidate
    {
        /// <summary>
        ///  Id of the Candidate
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Candidate of the Name
        /// </summary>
        [JsonProperty("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Candidate of the PartyId
        /// </summary>
        [JsonProperty("PartyId")]
        public int PartyId { get; set; }

        /// <summary>
        ///  Candidate state id 
        /// </summary>
        [JsonProperty("StateId")]
        public int StateId { get; set; }
    }
}
