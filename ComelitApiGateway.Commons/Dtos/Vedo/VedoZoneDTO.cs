using ComelitApiGateway.Commons.Enums.Vedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo
{
    /// <summary>
    /// This class describe a Zone.
    /// </summary>
    public class VedoZoneDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = String.Empty;
        public int AreaId { get; set; }
        public bool Hidden { get; set; }
        public VedoZoneStatusEnum Status { get; set; }
        public string StatusDescription { get; set; }
    }
}
