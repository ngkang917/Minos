using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Minos.SocketServer.V1.Define
{
    [JsonObject("ServerSetting")]
    public class SeverSetting
    {
        [JsonProperty("MaxConnectionCount")]
        public int MaxConnectionCount { get; set; }

        [JsonProperty("BufferSize")]
        public int BufferSize { get; set; }

        [JsonProperty("IPAddress")]
        public string IPAddress { get; set; }

        [JsonProperty("Port")]
        public int Port { get; set; }

        [JsonProperty("BackLog")]
        public int BackLog { get; set; }
    }
}
