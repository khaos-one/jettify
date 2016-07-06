using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Jettify.Serve {
    public sealed class AsyncTcpServerFunc
        : AsyncTcpServer {
        #region Self Fields & Props

        private readonly Func<Socket, CancellationToken, Task> _clientHandler;

        #endregion

        #region Constructors

        public AsyncTcpServerFunc(IPAddress ip, int port, Func<Socket, CancellationToken, Task> clientHandler,
            int? sendTimeout = null, int? receiveTimeout = null,
            int? bufferSize = null, bool keepAlive = false, bool dontLinger = true, string serverName = "AsyncTcpServer")
            : base(ip, port, sendTimeout, receiveTimeout, bufferSize, keepAlive, dontLinger, serverName) {
            _clientHandler = clientHandler;
        }

        public AsyncTcpServerFunc(int port, Func<Socket, CancellationToken, Task> clientHandler, int? sendTimeout = null,
            int? receiveTimeout = null, int? bufferSize = null,
            bool keepAlive = false, bool dontLinger = true, string serverName = "AsyncTcpServer")
            : base(port, sendTimeout, receiveTimeout, bufferSize, keepAlive, dontLinger, serverName) {
            _clientHandler = clientHandler;
        }

        #endregion

        #region AsyncTcpServer Impl

        protected override async Task HandleClient(Socket s, CancellationToken ct) {
            await _clientHandler(s, ct);
        }

        #endregion
    }
}
