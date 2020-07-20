using FreeNet;
using Minos.SocketServer.V1.User;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace Minos.SocketServer.V1
{

    class MinosServer
    {
        List<MinosDevice> devices;
        private static readonly Lazy<MinosServer> _instance = new Lazy<MinosServer>(() => new MinosServer());

        //private 생성자 
        private MinosServer()
        {
            if (devices == null)
            {
                devices = new List<MinosDevice>();
                Service = new CNetworkService(false);
                Service.session_created_callback += on_session_created;
            }
        }

        public static MinosServer Instance { get { return _instance.Value; } }
        public CNetworkService Service { get; private set; }

        public void Start(Config c)
        {
            Service.initialize(c._maxConnectionCount, c._bufferSize);
            Service.listen(c._ip, c._port, c._backLog);
        }


        public void on_session_created(CUserToken token)
        {
            MinosDevice device = new MinosDevice(token);
            lock (devices)
            {
                devices.Add(device);
            }
        }

        public void RemoveDevice(MinosDevice device)
        {
            lock (devices)
            {
                devices.Remove(device);
            }
        }

    }
}
