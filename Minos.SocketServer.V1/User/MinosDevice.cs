using FreeNet;
using Minos.SocketServer.V1.Define;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Minos.SocketServer.V1.User
{
    class MinosDevice : IPeer
    {
        CUserToken token;
        public MinosDevice(CUserToken token)
        {
            this.token = token;
            this.token.set_peer(this);
        }

        void IPeer.disconnect()
        {
            if (this.token != null)
            {
                this.token.ban();
                this.token = null;
            }
        }

        void IPeer.on_message(CPacket msg)
        {
            switch ((Protocol)msg.MIS_CMD)
            {
                case Protocol.SERVER_CHK:
                    //로그 작성
                    DoEcho(msg);
                    break;

                case Protocol.LOG_DATA:
                    //로그 작성
                    DoEcho(msg);
                    break;

                case Protocol.RFID_SEND:
                    DoEcho(msg);
                    break;

                case Protocol.CENTER_DB_CALL:
                    //로그 작성
                    DoEcho(msg);
                    break;

                case Protocol.USER_DB_CALL:
                    //로그 작성
                    DoEcho(msg);
                    break;

                case Protocol.FW_CALL:
                    DoEcho(msg);
                    break;

                default:
                    break;
            }

            // 응답 관련 함수는 여기에 정리하도록
        }

        void IPeer.on_removed()
        {
            MinosServer.Instance.RemoveDevice(this);
        }

        public void send(CPacket msg)
        {
            //msg.record_size();    헤더 정보를 변경하지 말것.
            this.token.send(new ArraySegment<byte>(msg.Buffer, 0, msg.Position));
        }

        public void send(ArraySegment<byte> data)
        {
            this.token.send(data);
        }

        public void DoEcho(CPacket msg)
        {
            string log = $"FROM CLIENT >> COMMAND:{msg.MIS_CMD}, MAC:{msg.MIS_MAC_ADDRESS} - ";
            string str = "";
            for (int i = 0; i < msg.Buffer.Length; i++)
            {
                str += str.Length > 0
                    ? " " + (msg.Buffer[i].ToString().Length == 1 ? "0" + msg.Buffer[i].ToString() : msg.Buffer[i].ToString())
                    : (msg.Buffer[i].ToString().Length == 1 ? "0" + msg.Buffer[i].ToString() : msg.Buffer[i].ToString());
            }
            Console.WriteLine($"{log} {str}");


            CPacket response = CPacket.Create(msg.Buffer);
            send(response);
        }

    }
}
