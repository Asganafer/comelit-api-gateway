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
        /// <summary>
        /// Alarm not entered
        /// </summary>
        NotEntered = 0,
        /// <summary>
        /// When alarm is entering in active status (ex. dalay)
        /// </summary>
        Activating = 1,
        /// <summary>
        /// Inserted alarm in all areas
        /// </summary>
        Active = 10,
        /// <summary>
        /// Inserted alarm in some areas
        /// </summary>
        PartialActive = 11,
        /// <summary>
        /// Emergency! Alarm is ringing
        /// </summary>
        Alarm = 20,

        Unknown = -1
    }
}
