using ComelitApiGateway.Commons.Enums.Vedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Dtos.Vedo
{
    /// <summary>
    /// This class describe a status of Area.
    /// </summary>
    public class VedoAreaStatusDTO : VedoAreaDTO
    {
        public AlarmStatusEnum Status { get; set; }
        public string StatusDescription { get { return Status.ToString(); } }

        public bool Ready { get; set; }
        public bool Armed { get; set; }
        public bool Alarm { get; set; }
        public bool AlarmMemory { get; set; }
        public bool Sabotage { get; set; }
        public bool Anomaly { get; set; }
        public bool InTime { get; set; }
        /// <summary>
        /// True when alarm is delayed and is waiting activation countdown
        /// </summary>
        public bool OutTime { get; set; }
        
    }
}
