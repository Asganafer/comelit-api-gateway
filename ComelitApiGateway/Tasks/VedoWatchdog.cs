using System;
using System.Threading;
using System.Threading.Tasks;
using ComelitApiGateway.Commons.Dtos;
using ComelitApiGateway.Commons.Dtos.Vedo;
using ComelitApiGateway.Commons.Interfaces;

namespace ComelitApiGateway.Tasks
{
    public class VedoWatchdog : BackgroundService
    {
        private readonly ILogger<VedoWatchdog> _logger;
        private readonly IComelitVedo _vedo;
        private readonly IVedoEventDispatcher _event;

        #region Cache

        private static VedoStatusDto _globalStatusCache;
        private static List<VedoAreaStatusDTO> _zonesStatusCache;

        #endregion

        public VedoWatchdog(ILogger<VedoWatchdog> logger, IComelitVedo vedo, IVedoEventDispatcher @event)
        {
            _logger = logger;
            _vedo = vedo;
            _event = @event;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("VedoWatchdog is starting.");

            stoppingToken.Register(() => _logger.LogInformation("VedoWatchdog is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("VedoWatchdog is doing background work.");

                //Check global alarm status
                Task.WaitAll([CheckGlobalAlarmStatus(), CheckZonesStatus()], cancellationToken: stoppingToken);

                //Check each seconds
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }

            _logger.LogInformation("VedoWatchdog has stopped.");
        }

        private async Task CheckGlobalAlarmStatus()
        {
            try
            {
                var alarmStatus = await _vedo.GetGlobalAlarmStatus();

                //if alarm is changed send new event
                if (_globalStatusCache != null && !_globalStatusCache.Equals(alarmStatus))
                {
                    await _event.OnChangeAlarmStatusAsync(alarmStatus);
                }

                //update cache
                _globalStatusCache = alarmStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking global alarm status.");
            }
        }

        private async Task CheckZonesStatus()
        {
            try
            {
                var areasStatus = await _vedo.GetAreasStatus();

                //if alarm is changed send new event
                if (_zonesStatusCache != null)
                {
                    //foreach zone loop and check if something is changed
                    foreach (var areaStatus in areasStatus)
                    {
                        var cachedStatus = _zonesStatusCache.FirstOrDefault(z => z.Id == areaStatus.Id);
                        if (cachedStatus != null && !cachedStatus.Equals(areaStatus))
                        {
                            await _event.OnChangeAreaStatusAsync(areaStatus);
                        }
                    }
                }

                //update cache
                _zonesStatusCache = areasStatus;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking global alarm status.");
            }
        }
    }
}