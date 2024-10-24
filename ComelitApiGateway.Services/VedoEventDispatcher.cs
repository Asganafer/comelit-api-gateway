using System.Text.Json;
using ComelitApiGateway.Commons;
using ComelitApiGateway.Commons.Dtos;
using ComelitApiGateway.Commons.Dtos.Events;
using ComelitApiGateway.Commons.Dtos.Vedo;
using ComelitApiGateway.Commons.Dtos.Vedo.ComelitSystem;
using ComelitApiGateway.Commons.Interfaces;
using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;

namespace ComelitApiGateway.Services
{
    public class VedoEventDispatcher : IVedoEventDispatcher, IDisposable
    {
        private IMqttClient _mqttClient;
        private IConfiguration _config;
        private bool _disposed = false;
        private bool dispatchEvents = false;

        public VedoEventDispatcher(IConfiguration config)
        {
            //Verify if MQTT is enabled
            dispatchEvents = Convert.ToBoolean(config["MQTT_ENABLED"]?.ToString() ?? "false");
            _config = config;
        }
        public async Task InitializeAsync()
        {
            if (dispatchEvents)
            {

                EventConfigurationDto eventConfiguration = new EventConfigurationDto()
                {
                    BrokerAddress = _config["MQTT_IP"]?.ToString() ?? "",
                    BrokerPort = Convert.ToInt32(_config["MQTT_PORT"]?.ToString() ?? "0"),
                    ClientId = "ComelitApiGateway",
                    Username = _config["MQTT_USERNAME"]?.ToString() ?? "",
                    Password = _config["MQTT_PASSWORD"]?.ToString() ?? "",
                };

                VerifyConfiguration(eventConfiguration);

                if (_mqttClient == null)
                {

                    var factory = new MqttFactory();
                    _mqttClient = factory.CreateMqttClient();
                }


                if (!_mqttClient.IsConnected)
                {
                    var options = new MqttClientOptionsBuilder()
                    .WithClientId(eventConfiguration.ClientId)
                    .WithTcpServer(eventConfiguration.BrokerAddress, eventConfiguration.BrokerPort)
                    .WithCredentials(eventConfiguration.Username, eventConfiguration.Password)
                    .Build();

                    await _mqttClient.ConnectAsync(options, CancellationToken.None);
                }
            }



        }

        public async Task OnChangeAlarmStatusAsync(VedoStatusDto globalStatus)
        {
            if (dispatchEvents)
            {
                await InitializeAsync();
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("/comelit/vedo/alarm/change")
                    .WithPayload(JsonSerializer.Serialize(globalStatus))
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .WithRetainFlag(false)
                    .Build();

                await _mqttClient.PublishAsync(message, CancellationToken.None);
            }
        }

        public async Task OnChangeAreaStatusAsync(VedoAreaStatusDTO areaStatus)
        {
            if (dispatchEvents)
            {
                await InitializeAsync();
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("/comelit/vedo/alarm/area/change")
                    .WithPayload(JsonSerializer.Serialize(areaStatus))
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .WithRetainFlag(false)
                    .Build();

                await _mqttClient.PublishAsync(message, CancellationToken.None);
            }
        }

        #region Private

        private void VerifyConfiguration(EventConfigurationDto eventConfiguration)
        {
            if (string.IsNullOrEmpty(eventConfiguration.ClientId) ||
                string.IsNullOrEmpty(eventConfiguration.BrokerAddress) ||
                eventConfiguration.BrokerPort == 0 ||
                string.IsNullOrEmpty(eventConfiguration.Username) ||
                string.IsNullOrEmpty(eventConfiguration.Password))
            {
                dispatchEvents = false;
            }
            else
            {
                dispatchEvents = true;
            }
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _mqttClient?.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}