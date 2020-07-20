using System;
using System.Collections.Generic;
using System.Text;

namespace Minos.SocketServer.V1.Define
{
    public enum Protocol : short
    {
        Begin = 0,

        Chat_MSG_Req = 1,
        Chat_MSG_Ack = 2,

        END
    }
}
