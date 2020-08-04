using Newtonsoft.Json;

namespace Minos.SocketClient
{
    [JsonObject("ClientSetting")]
    public class ClientSetting
    {
        [JsonProperty("IPAddress")]
        public string IPAddress { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }
    }
}
