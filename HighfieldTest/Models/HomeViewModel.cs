using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HighfieldTest.Models
{
    public class HomeViewModel
    {
        [JsonPropertyName("users")]
        public List<User> users { get; set; }
        [JsonPropertyName("ages")]
        public IEnumerable<Age> ages { get; set; }
        [JsonPropertyName("topColours")]
        public IEnumerable<TopColour> topColours { get; set; }

    }
}
