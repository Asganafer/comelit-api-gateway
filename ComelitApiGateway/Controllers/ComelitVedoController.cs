using ComelitApiGateway.Commons.Dtos.Vedo;
using ComelitApiGateway.Commons.Enums.Vedo;
using ComelitApiGateway.Commons.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ComelitApiGateway.Controllers
{
    [ApiController]
    [Route("Vedo")]
    public class ComelitVedoController : BaseController
    {
        protected readonly IComelitVedo _vedo;
        public ComelitVedoController(IConfiguration config, IComelitVedo vedo) : base(config)
        {
            _vedo = vedo;
        }

        #region GET

        /// <summary>
        /// Get general status of alarm
        /// </summary>
        /// <returns></returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetGeneralStatus()
        {
            var areas = await _vedo.GetAreasStatus();
            if (areas.Any(x => x.Alarm))
            {
                return Ok(new
                {
                    Id = AlarmStatusEnum.Alarm,
                    Description = AlarmStatusEnum.Alarm.ToString()
                });
            }
            else if (areas.All(x => x.Status == AlarmStatusEnum.Active))
            {
                return Ok(new
                {
                    Id = AlarmStatusEnum.Active,
                    Description = AlarmStatusEnum.Active.ToString()
                });
            }
            else if (areas.Any(x => x.Armed))
            {
                return Ok(new
                {
                    Id = AlarmStatusEnum.PartialActive,
                    Description = AlarmStatusEnum.PartialActive.ToString()
                });
            }
            else
            {
                return Ok(new
                {
                    Id = AlarmStatusEnum.NotEntered,
                    Description = AlarmStatusEnum.NotEntered.ToString()
                });
            }
            
        }

        /// <summary>
        /// Return list of areas with alarm status
        /// </summary>
        /// <returns></returns>
        [HttpGet("areas")]
        public async Task<IActionResult> GetStatusAsync()
        {
            return Ok(await _vedo.GetAreasStatus());
        }

        /// <summary>
        /// Get status of specific area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("areas/{areaId}")]
        public async Task<IActionResult> GetAreaStatus(int areaId)
        {
            return Ok((await _vedo.GetAreasStatus()).FirstOrDefault(x=> x.Id == areaId));
        }

        /// <summary>
        /// Return true if alarm of area is enabled 
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("areas/{areaId}/is-active")]
        public async Task<IActionResult> IsAlarmAreaActive(int areaId)
        {
            var areaStatus = (await _vedo.GetAreasStatus()).FirstOrDefault(x => x.Id == areaId);
            if (areaStatus == null) return Ok(false);
            return Ok(areaStatus.Status == AlarmStatusEnum.Active || areaStatus.Status == AlarmStatusEnum.Activating);
        }

        /// <summary>
        ///  Return true if alarm of all areas is enabled 
        /// </summary>
        /// <returns></returns>
        [HttpGet("areas/all/is-active")]
        public async Task<IActionResult> IsAlarmActive()
        {
            return Ok((await _vedo.GetAreasStatus()).Any(x=> x.Status == AlarmStatusEnum.Active || x.Status == AlarmStatusEnum.PartialActive));
        }

        /// <summary>
        /// Get list of zones of specific area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpGet("areas/{areaId}/zones")]
        public async Task<IActionResult> GetZonesStatus(int areaId)
        {
            return Ok(await _vedo.GetZoneList(areaId));
        }

        /// <summary>
        /// Get list of all zones (section of area)
        /// </summary>
        /// <returns></returns>
        [HttpGet("areas/all/zones")]
        public async Task<IActionResult> GetAllZonesStatus()
        {
            return Ok(await _vedo.GetZoneList(-1));
        }

        #endregion

        #region CRUD

        /// <summary>
        /// Enable alarm of all areas
        /// </summary>
        /// <returns></returns>
        [HttpPost("areas/all/arm")]
        public async Task<IActionResult> ArmAllAreas()
        {
            return Ok(await _vedo.ArmAlarm());
        }

        /// <summary>
        /// Enable alarm of specific area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpPost("areas/{areaId}/arm")]
        public async Task<IActionResult> ArmArea(int areaId)
        {
            return Ok(await _vedo.ArmAlarm(areaId));
        }

        /// <summary>
        /// Disable alarm of all areas
        /// </summary>
        /// <returns></returns>
        [HttpPost("areas/all/disarm")]
        public async Task<IActionResult> DisarmAllAreas()
        {
            return Ok(await _vedo.DisarmAlarm());
        }

        /// <summary>
        /// Disable alarm of specific area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpPost("areas/{areaId}/disarm")]
        public async Task<IActionResult> DisarmArea(int areaId)
        {
            return Ok(await _vedo.DisarmAlarm(areaId));
        }

        /// <summary>
        /// Toggle alarm of all areas
        /// </summary>
        /// <returns>True = alarm inserted, False = alarm disabled</returns>
        [HttpPost("areas/all/arm-disarm")]
        public async Task<IActionResult> ArmDisarmAllAreas()
        {
            if ((await _vedo.GetAreasStatus()).Any(x => x.Armed))
            {
                await _vedo.DisarmAlarm();
                return Ok(false);
            }
            else
            {
                await _vedo.ArmAlarm();
                return Ok(true);
            }
        }

        /// <summary>
        /// Toggle alarm ofspecific area
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        [HttpPost("areas/{areaId}/arm-disarm")]
        public async Task<IActionResult> ArmDisarmAllAreas(int areaId)
        {
            var area = (await _vedo.GetAreasStatus()).FirstOrDefault(x=> x.Id == areaId);
            if (area != null)
            {
                if (area.Armed)
                {
                    await _vedo.DisarmAlarm(areaId);
                    return Ok(false);
                }
                else
                {
                    await _vedo.ArmAlarm(areaId);
                    return Ok(true);
                }
            }
            else
            {
                return Ok(false);
            }
        }

        /// <summary>
        /// Exclude zone from area (ex. windows, door, sensor, etc..).
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [HttpPost("zones/{zoneId}/exclude")]
        public async Task<IActionResult> ExcludeZone(int zoneId)
        {
            return Ok(await _vedo.ExcludeZone(zoneId));
        }

        /// <summary>
        /// Include zone that has been excluded
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [HttpPost("zones/{zoneId}/include")]
        public async Task<IActionResult> IncludeZone(int zoneId)
        {
            return Ok(await _vedo.IncludeZone(zoneId));
        }

        /// <summary>
        /// Isolate zone
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [HttpPost("zones/{zoneId}/isolate")]
        public async Task<IActionResult> IsolateZone(int zoneId)
        {
            return Ok(await _vedo.IsolateZone(zoneId));
        }

        /// <summary>
        /// Remove zone from list of isolated devices
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        [HttpPost("zones/{zoneId}/remove-isolate")]
        public async Task<IActionResult> UnisolateZone(int zoneId)
        {
            return Ok(await _vedo.UnisolateZone(zoneId));
        }

        #endregion
    }
}
