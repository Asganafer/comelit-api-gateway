using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem
{
    /// <summary>
    /// Response of vedo's zone description
    /// </summary>
    public class ZoneDescriptionResponseDTO : BaseVedoResponse
    {
        [JsonPropertyName("description")]
        public List<string> ZoneNames { get; set; } = new List<string>();

        [JsonPropertyName("in_area")]
        public List<int> InArea { get; set; } = new List<int>();

        [JsonPropertyName("present")]
        public string Present { get; set; } = "";
    }
}
