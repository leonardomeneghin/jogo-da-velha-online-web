using System.Diagnostics;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace webServer.Services.SocketDependency
{
    //TODO: Fazer essa classe herdar uma interface de implementação para ser possível realizar uma troca entre o uso de um framework e a implementação manual
    public class WebSocketServer
    {
        public HttpListener httpListener { get; private set; }
        public WebSocketServer(string host)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost/connect");
            httpListener.Start();
            Debugger.Log(0, "APP_LOAD", "Listening websocket connections");
        }
        public async void Start()
        {
            var context = await httpListener.GetContextAsync();
            if(context.Request.IsWebSocketRequest)
            {
                var webSocketContext = await context.AcceptWebSocketAsync(subProtocol: null);
                var webSocket = webSocketContext.WebSocket;

                Debugger.Log(1, "CLIENT_CONNECT", "Client connected");

                var receiveBuffer = new byte[1024];
                if(webSocket.State == WebSocketState.Open)
                {
                    var receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                    if(receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        var receivedMessage = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                        Debugger.Log(1, "CLIENT_INFO", $"Message Received: {receivedMessage}");

                        var buffer = Encoding.UTF8.GetBytes($"Hello from server"); //TODO: INFO Talvez aqui seja necessário elaborar a resposta aos clientes.
                        await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    } //Tratar outros tipos de mensagens recebidas além de Text
                    else if(receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                        Debugger.Log(1, "CLIENT_CONNECT", "Connection was closed");
                    }
                }

            }
        }
    }
}
