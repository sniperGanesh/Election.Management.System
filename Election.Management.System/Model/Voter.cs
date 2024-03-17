using Newtonsoft.Json;

namespace Election.Management.System.Models
{
    public class Voter
    {
        /// <summary>
        /// Id Of the Voter
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Person VoterIDs
        /// </summary>
        [JsonProperty("VoterId")]
        public string? VoterId { get; set; }

        /// <summary>
        /// Name of the person for Voting
        /// </summary>
        [JsonProperty("Name")]
        public string? Name { get; set; }

        /// <summary>
        /// Address of the person
        /// </summary>
        [JsonProperty("Address")]
        public string? Address { get; set; }
        /// <summary>
        /// Photo of the person
        /// </summary>
        [JsonProperty("Photo")]
        public byte[]? Photo { get; set; }
        /// <summary>
        /// Status of the Voter to Vote or Not
        /// </summary>

        [JsonProperty("IsApproved")]
        public bool IsApproved { get; set; }
    }
}
