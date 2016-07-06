using System;
using System.Net;
using System.Net.Sockets;

namespace Jettify.Serve {
    public class TcpThreadedServerFunc
        : TcpThreadedServer {

        #region Self Fields & Props

        protected Action<Socket> ClientHandler;
        protected Action<Socket, Exception> ErrorHandler;

        #endregion

        #region Constructors

        public TcpThreadedServerFunc(IPAddress ip, int port, Action<Socket> clientHandler,
            Action<Socket, Exception> errorHandler = null, bool keepAlive = false, bool dontLinger = true,
            int? sendTimeout = null, int? receiveTimeout = null, string serverName = "", int? bufferSize = null)
            : base(ip, port, keepAlive, dontLinger, sendTimeout, receiveTimeout, serverName, bufferSize) {
            ClientHandler = clientHandler;
            ErrorHandler = errorHandler;
        }

        public TcpThreadedServerFunc(int port, Action<Socket> clientHandler,
            Action<Socket, Exception> errorHandler = null) : base(port) {
            ClientHandler = clientHandler;
            ErrorHandler = errorHandler;
        }

        #endregion

        #region TcpThreadedServer Impl

        protected override void HandleClient(Socket socket) {
            ClientHandler(socket);
        }

        protected override void HandleError(Socket socket, Exception e) {
            ErrorHandler?.Invoke(socket, e);
        }

        #endregion
    }
}
