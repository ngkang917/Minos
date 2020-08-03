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
            Protocol p = (Protocol)msg.MIS_CMD;
            switch (p)
            {
                case Protocol.SERVER_CHK:
                    //로그 작성
                    DoAction(msg);
                    break;

                case Protocol.LOG_DATA:
                    //로그 작성
                    DoAction(msg);
                    break;

                case Protocol.RFID_SEND:
                    break;

                case Protocol.CENTER_DB_CALL:
                    //로그 작성
                    DoAction(msg);
                    break;

                case Protocol.USER_DB_CALL:
                    //로그 작성
                    DoAction(msg);
                    break;

                case Protocol.FW_CALL:
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

        public void Socket_TxBuff_String_Addr(ref byte[] buffer, ref uint point_tarket_position, string value, int Str_Cnt)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = Encoding.GetEncoding("ksc_5601").GetBytes(value);
            int length = bytes.Length;
            Buffer.BlockCopy((Array)bytes, 0, (Array)buffer, (int)point_tarket_position, length);
            point_tarket_position += (uint)length;
            for (int index = length; index < Str_Cnt; ++index)
                buffer[(int)point_tarket_position++] = (byte)0;
        }

        public void Socket_TxBuff_UInt16_Add(ref byte[] buffer, ref uint point_tarket_position, ushort buff)
        {
            buffer[(int)point_tarket_position++] = (byte)buff;
            buffer[(int)point_tarket_position++] = (byte)((uint)buff >> 8);
        }

        public void Socket_TxBuff_UInt32_Add(ref byte[] buffer, ref uint point_tarket_position, uint buff)
        {
            Socket_TxBuff_UInt16_Add(ref buffer, ref point_tarket_position, (ushort)buff);
            Socket_TxBuff_UInt16_Add(ref buffer, ref point_tarket_position, (ushort)(buff >> 16));
        }

        public void DoAction(CPacket msg)
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
