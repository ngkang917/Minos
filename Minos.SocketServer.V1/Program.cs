using FreeNet;
using System;

namespace Minos.SocketServer.V1
{
    class Program
    {
        static void Main(string[] args)
        {
            MinosServer server = MinosServer.Instance;
            
            server.Start(new Config()
            {
                _maxConnectionCount = 10000,
                _bufferSize = 1024,
                _ip = "0.0.0.0",
                _port = 7979,
                _backLog = 100
            });

            // 서버에서 하트비트 체크를 끌때 사용함.
            // 스트레스 테스트를 하기 위해 FreeNet이 아닌 다른 클라이언트를 쓰는 경우등에 필요할것 같다.
            // Remove below comments to disable heartbeat on server.
            // (It maybe use to stress test from another client program not using FreeNet.)
            //service.disable_heartbeat();

            Console.WriteLine("Started!");
            while (true)
            {
                //Console.Write(".");
                string input = Console.ReadLine();
                if (input.Equals("users"))
                {
                    Console.WriteLine(server.Service.usermanager.get_total_count());
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
