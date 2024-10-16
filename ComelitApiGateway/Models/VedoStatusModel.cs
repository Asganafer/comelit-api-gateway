using ComelitApiGateway.Commons.Enums.Vedo;

namespace ComelitApiGateway.Models
{
    /// <summary>
    /// State of the alarm (Comelit vedo)
    /// </summary>
    public class VedoStatusModel
    {
        /// <summary>
        /// ID of alarm state
        /// </summary>
        public AlarmStatusEnum Id { get; set; }
        /// <summary>
        /// Description of alarm state
        /// </summary>
        public string Description { get; set; } = "";
    }
}
