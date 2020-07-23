using System;
using System.Collections.Generic;
using System.Text;

namespace Minos.SocketServer.V1.Define
{
    public enum Protocol : short
    {
        SERVER_CHK = 1,
        LOG_DATA = 2,
        RFID_SEND = 3,
        CENTER_DB_CALL = 4,
        USER_DB_CALL = 5,
        FW_CALL = 6
    }
}
