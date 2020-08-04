using FreeNet;
using Microsoft.Extensions.Configuration;
using Minos.SocketServer.V1.Define;
using System;
using System.IO;

namespace Minos.SocketServer.V1
{
    class Program
    {
        static void Main(string[] args)
        {
            // appsetting.json 파일 내부 설정 정보를 활용하도록 구성
            var config = GetConfiguration().GetSection("ServerSetting").Get<SeverSetting>();

            // 소켓 서버 환경 구성
            MinosServer server = MinosServer.Instance;
            server.Start(new Config()
            {
                _maxConnectionCount = config.MaxConnectionCount,
                _bufferSize = config.BufferSize,
                _ip = config.IPAddress,
                _port = config.Port,
                _backLog = config.BackLog
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

        /// <summary>
        /// 환경 설정 정보를 가져오는 함수
        /// </summary>
        /// <returns>IConfiguration</returns>
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }
    }
}
