﻿using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Minos.SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = GetConfiguration().GetSection("ClientSetting").Get<ClientSetting>();

            // (1) 소켓 객체 생성 (TCP 소켓)
            uint point_tarket_position = 0;

            string START_CODE = "";
            string MAC_ADDRESS = "";
            ushort FW_VERSION = 0;
            ushort DB_VERSION = 0;
            ushort MISCOMMAND = 0;
            uint SENDDATASIZE = 0;
            ushort DATASOCKNO = 0;
            ushort DATACOUNTN = 0;
            ushort USERID = 0;
            ushort LOGTYPE = 0;
            uint LOGTIME = 0;
            uint data_point_tarket_position = 0;

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 서버에 연결
            var ep = new IPEndPoint(IPAddress.Parse(config.IPAddress), config.Port);
            sock.Connect(ep);

            string cmd = string.Empty;


            Console.WriteLine("Connected... Enter Q to exit");

            // Q 를 누를 때까지 계속 Echo 실행
            while ((cmd = Console.ReadLine()) != "Q")
            {
                byte[] buff = new byte[1024];
                byte[] dataBuff = new byte[512];
                switch (cmd)
                {
                    // Client to Server
                    // command 1: SERVER CHECK
                    case "1":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;

                    // Client to Server
                    // command 2: LOG DATA
                    case "2":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 62;
                        DATACOUNTN = 32;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DATASOCKNO);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DATACOUNTN);

                        for (int i = 0; i < DATACOUNTN / 8; i++)
                        {
                            Socket_TxBuff_UInt16_Add(ref dataBuff, ref data_point_tarket_position, Convert.ToUInt16(i));
                            Socket_TxBuff_UInt16_Add(ref dataBuff, ref data_point_tarket_position, Convert.ToUInt16(1));
                            Socket_TxBuff_UInt32_Add(ref dataBuff, ref data_point_tarket_position, Convert.ToUInt32((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
                        }

                        dataBuff.CopyTo(buff, 31);

                        break;

                    // Client to Server
                    // command 3: RFID SEND
                    case "3":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;

                    // Client to Server
                    // command 4: CENTER DB CALL
                    case "4":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;

                    // Client to Server
                    // command 5: USER DB CALL
                    case "5":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;

                    // Client to Server
                    // command 6: FW CALL
                    case "6":
                        START_CODE = "MIS_";
                        MAC_ADDRESS = "98D8638A033E";
                        FW_VERSION = 1;
                        DB_VERSION = 1;
                        MISCOMMAND = Convert.ToUInt16(cmd);
                        SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;

                    default:
                        Console.WriteLine("잘못 된 명령어 입니다!");
                        continue;
                }

                // (3) 서버에 데이타 전송
                sock.Send(buff, SocketFlags.None);

                // (4) 서버에서 데이타 수신
                byte[] receiverBuff = new byte[1024];
                int n = sock.Receive(receiverBuff);


                //int iMessageLength = BitConverter.ToInt32(receiverBuff, 22);

                string str = "";

                for (int i = 0; i < n; i++)
                {
                    str += str.Length > 0
                        ? " " + (receiverBuff[i].ToString().Length == 1 ? "0" + receiverBuff[i].ToString() : receiverBuff[i].ToString())
                        : (receiverBuff[i].ToString().Length == 1 ? "0" + receiverBuff[i].ToString() : receiverBuff[i].ToString());
                }
                Console.WriteLine(str);
                point_tarket_position = 0;
                data_point_tarket_position = 0;
            }
            // (5) 소켓 닫기
            sock.Close();
        }

        public static void Socket_TxBuff_String_Addr(ref byte[] buffer, ref uint point_tarket_position, string value, int Str_Cnt)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = Encoding.GetEncoding("ksc_5601").GetBytes(value);
            int length = bytes.Length;
            Buffer.BlockCopy((Array)bytes, 0, (Array)buffer, (int)point_tarket_position, length);
            point_tarket_position += (uint)length;
            for (int index = length; index < Str_Cnt; ++index)
                buffer[(int)point_tarket_position++] = (byte)0;
        }

        public static void Socket_TxBuff_UInt16_Add(ref byte[] buffer, ref uint point_tarket_position, ushort buff)
        {
            buffer[(int)point_tarket_position++] = (byte)buff;
            buffer[(int)point_tarket_position++] = (byte)((uint)buff >> 8);
        }

        public static void Socket_TxBuff_UInt32_Add(ref byte[] buffer, ref uint point_tarket_position, uint buff)
        {
            Socket_TxBuff_UInt16_Add(ref buffer, ref point_tarket_position, (ushort)buff);
            Socket_TxBuff_UInt16_Add(ref buffer, ref point_tarket_position, (ushort)(buff >> 16));
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
