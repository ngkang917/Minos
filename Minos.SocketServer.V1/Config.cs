namespace Minos.SocketServer.V1
{
    public struct Config
    {
        public int _maxConnectionCount;
        public int _bufferSize;

        public string _ip;
        public int _port;
        public int _backLog;

        public Config(int maxConnectionCount, int bufferSize, string ip, int port, int backLog)
        {
            _maxConnectionCount = maxConnectionCount;
            _bufferSize = bufferSize;
            _ip = ip;
            _port = port;
            _backLog = backLog;
        }
    }
}
