using ComelitApiGateway.Commons.Dtos.Vedo;
using ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComelitApiGateway.Commons.Interfaces
{
    public interface IComelitVedo
    {

        #region Vedo System Api Call

        /// <summary>
        /// Login and get Cookie UID
        /// </summary>
        /// <returns>Cookie UID</returns>
        Task<string> Login();

        /// <summary>
        /// Get decriptions of all Areas
        /// </summary>
        /// <returns></returns>
        Task<AreaDescriptionResponseDTO> ComelitGetAreasDescription();

        /// <summary>
        /// Get status of all the Areas
        /// </summary>
        /// <returns></returns>
        Task<AreaStatusResponseDTO> ComelitGetAreasStatus();

        /// <summary>
        /// Get status of all the Zones of each Areas
        /// </summary>
        /// <returns></returns>
        Task<ZoneStatusResponseDTO> ComelitGetZonesStatus();

        #endregion

        #region Api call wrapper

        /// <summary>
        /// Get list of Areas with their ID
        /// </summary>
        /// <returns></returns>
        Task<List<VedoAreaDTO>> GetAreasList();
        Task<List<VedoAreaStatusDTO>> GetAreasStatus();
        Task<List<VedoZoneDTO>> GetZoneList(int idArea = 0, bool removeHiddenZones = true);

        Task<bool> ArmAlarm(int? area = null, bool force = true);
        Task<bool> DisarmAlarm(int? area = null, bool force = true);
        Task<bool> ExcludeZone(int zoneId);
        Task<bool> IncludeZone(int zoneId);
        Task<bool> IsolateZone(int zoneId);
        Task<bool> UnisolateZone(int zoneId);
        #endregion
    }
}
