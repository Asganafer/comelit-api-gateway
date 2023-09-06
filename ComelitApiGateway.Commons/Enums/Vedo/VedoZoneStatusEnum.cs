using ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Enums.Vedo
{
    public enum VedoZoneStatusEnum
    {
        Ready = 0,
        Active = 32,
        /// <summary>
        /// Door or windows open
        /// </summary>
        Open = 1,
        Excluded = 128,
        Isolated = 256,
        Sabotated = 12,
        Alarm = 2,
        Inhibited = 32768,

        Unknown = -1
    }
}
