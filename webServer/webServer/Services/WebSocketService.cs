using webServer.Services.SocketDependency;

namespace webServer.Services
{

    public class WebSocketService
    {
        public static string Host { get; private set; }
        private static readonly Lazy<WebSocketService> _instance = new Lazy<WebSocketService>(() => new WebSocketService(Host));
        public static WebSocketService Instance => _instance.Value;

        private WebSocketServer _wssv;
        private WebSocketService(string host)
        {
            ArgumentNullException.ThrowIfNull(host);
            Host = host; //Hostname da conexão websocket
            _wssv = new WebSocketServer(Host);
            _wssv.Start();
        }
    }
}
