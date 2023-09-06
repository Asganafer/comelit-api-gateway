using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem
{
    /// <summary>
    /// Response of vedo's zone status
    /// </summary>
    public class ZoneStatusResponseDTO : BaseVedoResponse
    {
        [JsonPropertyName("status")]
        public string ZoneStatus { get; set; } = "";
    }
}
