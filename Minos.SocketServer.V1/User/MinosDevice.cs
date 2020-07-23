using FreeNet;
using Minos.SocketServer.V1.Define;
using System;
using System.Collections.Generic;
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
            Protocol p = (Protocol)msg.pop_protocol_id();

            switch (p)
            {
                case Protocol.SERVER_CHK:
                    {
                        string text = msg.pop_start_code();
                        Console.WriteLine(text);

                        CPacket response = CPacket.Create((ushort)Protocol.SERVER_CHK);
                        response.push(text);
                        send(response);

                        if (text.Equals("exit"))
                        {
                            // 대량의 메시지를 한꺼번에 보낸 후 종료하는 시나리오 테스트.
                            for (int i = 0; i < 1000; ++i)
                            {
                                CPacket dummy = CPacket.Create((ushort)Protocol.SERVER_CHK);
                                dummy.push(i.ToString());
                                send(dummy);
                            }

                            this.token.ban();
                        }
                    }
                    break;
            }

        }

        void IPeer.on_removed()
        {
            MinosServer.Instance.RemoveDevice(this);
        }

        public void send(CPacket msg)
        {
            msg.record_size();
            this.token.send(new ArraySegment<byte>(msg.Buffer, 0, msg.Position));
        }

        public void send(ArraySegment<byte> data)
        {
            this.token.send(data);
        }
    }
}
