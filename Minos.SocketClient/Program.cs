using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Minos.SocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // (1) 소켓 객체 생성 (TCP 소켓)
            uint point_tarket_position = 0;

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // (2) 서버에 연결
            var ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7979);
            sock.Connect(ep);

            string cmd = string.Empty;
            

            Console.WriteLine("Connected... Enter Q to exit");

            // Q 를 누를 때까지 계속 Echo 실행
            while ((cmd = Console.ReadLine()) != "Q")
            {
                byte[] buff = new byte[1024];
                switch (cmd)
                {
                    case "1":
                        string START_CODE = "MIS_";
                        string MAC_ADDRESS = "98D8638A033E";
                        ushort FW_VERSION = 1;
                        ushort DB_VERSION = 1;
                        ushort MISCOMMAND = 1;
                        uint SENDDATASIZE = 26;
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, START_CODE, START_CODE.Length);
                        Socket_TxBuff_String_Addr(ref buff, ref point_tarket_position, MAC_ADDRESS, START_CODE.Length);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, FW_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, DB_VERSION);
                        Socket_TxBuff_UInt16_Add(ref buff, ref point_tarket_position, MISCOMMAND);
                        Socket_TxBuff_UInt32_Add(ref buff, ref point_tarket_position, SENDDATASIZE);
                        break;
                    case "2":

                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
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
    }



}
