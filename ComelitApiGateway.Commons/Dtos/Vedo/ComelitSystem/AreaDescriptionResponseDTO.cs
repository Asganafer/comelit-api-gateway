using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem
{
    /// <summary>
    /// Response of vedo's status
    /// </summary>
    public class AreaDescriptionResponseDTO : BaseVedoResponse
    {
        [JsonPropertyName("description")]
        public List<string> AreaNames { get; set; } = new List<string>();

        [JsonPropertyName("p1_pres")]
        public List<int> P1Pres { get; set; } = new List<int>();

        [JsonPropertyName("p2_pres")]
        public List<int> P2Pres { get; set; } = new List<int>();
    }
}
