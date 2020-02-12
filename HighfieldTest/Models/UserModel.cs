using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HighfieldTest.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
        [JsonPropertyName("firstName")]
        public string firstName { get; set; }
        [JsonPropertyName("lastName")]
        public string lastName { get; set; }
        [JsonPropertyName("email")]
        public string email { get; set; }
        [JsonPropertyName("dob")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime dob { get; set; }
        [JsonPropertyName("favouriteColour")]
        public string favouriteColour { get; set; }
    }
    public class Age
    {
        [JsonPropertyName("userId")]
        public string userId { get; set; }
        [JsonPropertyName("originalAge")]
        public int originalAge { get; set; }
        [JsonPropertyName("agePlusTwenty")]
        public int agePlusTwenty { get; set; }
    }
    public class TopColour
    {
        [JsonPropertyName("colour")]
        public string colour { get; set; }
        [JsonPropertyName("count")]
        public int count { get; set; }
    }

}
