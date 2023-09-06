using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem
{
    public class BaseVedoResponse
    {
        [JsonPropertyName("logged")]
        public int Logged { get; set; }

        [JsonPropertyName("rt_stat")]
        public int Status { get; set; }

        [JsonPropertyName("vedo_auth")]
        public int[] VedoAuth { get; set; } = Array.Empty<int>();

        [JsonPropertyName("life")]
        public int Life { get; set; }

        [JsonPropertyName("Area_open")]
        public int AreaOpen { get; set; }
    }
}
