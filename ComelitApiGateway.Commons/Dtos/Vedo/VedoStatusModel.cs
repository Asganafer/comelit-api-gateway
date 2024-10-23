using ComelitApiGateway.Commons.Enums.Vedo;

namespace ComelitApiGateway.Commons.Dtos
{
    /// <summary>
    /// State of the alarm (Comelit vedo)
    /// </summary>
    public class VedoStatusDto
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
