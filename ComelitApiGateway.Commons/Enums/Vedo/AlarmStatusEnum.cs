using ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Enums.Vedo
{
    public enum AlarmStatusEnum
    {
        NotEntered = 0,
        /// <summary>
        /// When alarm is entering in active status (ex. dalay)
        /// </summary>
        Activating = 1,

        Active = 10,
        PartialActive = 11,
        
        Alarm = 20,

        Unknown = -1
    }
}
