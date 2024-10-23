
namespace ComelitApiGateway.Commons.Dtos.Events
{
    public class EventConfigurationDto
    {
        #region Broker configuration
        public string ClientId { get; set; }

        public string BrokerAddress{ get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? BrokerPort { get; set; }

        #endregion

        #region Settings
        public bool DispatchEventOnGlobalAlarmChange { get; set; }
        public bool DispatchEventOnZoneStatusChange { get; set; }

        #endregion
    }
}