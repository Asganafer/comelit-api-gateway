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
    public class AreaStatusResponseDTO : BaseVedoResponse
    {
        [JsonPropertyName("ready")]
        public List<int> ReadyAreas { get; set; } = new List<int>();

        [JsonPropertyName("armed")]
        public List<int> ArmedAreas { get; set; } = new List<int>();

        [JsonPropertyName("alarm")]
        public List<int> AlarmedAreas { get; set; } = new List<int>();

        [JsonPropertyName("alarm_memory")]
        public List<int> LastAlarmedAreas { get; set; } = new List<int>();

        [JsonPropertyName("sabotage")]
        public List<int> SabotatedAreas { get; set; } = new List<int>();

        [JsonPropertyName("anomaly")]
        public List<int> AnomalyAreas { get; set; } = new List<int>();

        [JsonPropertyName("in_time")]
        public List<int> InTimeAreas { get; set; } = new List<int>();

        /// <summary>
        /// True when insert of alarm is delayed
        /// </summary>
        [JsonPropertyName("out_time")]
        public List<int> OutTimeAreas { get; set; } = new List<int>();

    }
}
