using ComelitApiGateway.Commons.Dtos;
using ComelitApiGateway.Commons.Dtos.Events;
using ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem;

namespace ComelitApiGateway.Commons.Interfaces
{
    /// <summary>
    /// Dispatch event
    /// </summary>
    public interface IVedoEventDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IEventDispatcher"/> interface.
        /// </summary>
        /// <param name="eventConfiguration">The event configuration.</param>
        Task InitializeAsync(EventConfigurationDto eventConfiguration);
        
        /// <summary>
        /// Dispatch event about change of global alarm status
        /// </summary>
        /// <param name="globalStatus"></param>
        Task OnChangeAlarmStatusAsync(VedoStatusDto globalStatus);

        /// <summary>
        /// Dispatch event when status of zone change
        /// </summary>
        /// <param name="globalStatus"></param>
        Task OnChangeZoneStatusAsync(VedoZoneStatusDTO zoneStatus);
    }
}