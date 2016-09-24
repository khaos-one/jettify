using System;
using System.Net;
using System.Net.Sockets;
using Jettify.Extensions;
using Jettify.Logging;

namespace Jettify.Serve {
    public abstract class TcpThreadedServer
        : ThreadedServer {

        #region Self Fields & Props

        protected Socket ListenerSocket;

        public IPEndPoint LocalEndPoint { get; protected set; }
        public int? ClientSocketSendTimeout { get; protected set; }
        public int? ClientSocketReceiveTimeout { get; protected set; }
        public int? ClientSocketBufferSize { get; protected set; }
        public bool KeepAlive { get; protected set; }
        public bool DontLinger { get; protected set; }

        public int Port => LocalEndPoint.Port;

        #endregion

        #region Constructors

        protected TcpThreadedServer(IPAddress ip, int port, bool keepAlive = false, bool dontLinger = true,
            int? sendTimeout = null, int? receiveTimeout = null, string serverName = "", int? bufferSize = null) {
            ClientSocketSendTimeout = sendTimeout;
            ClientSocketReceiveTimeout = receiveTimeout;
            ClientSocketBufferSize = bufferSize;
            KeepAlive = keepAlive;
            DontLinger = dontLinger;

            LocalEndPoint = new IPEndPoint(ip, port);
            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try {
                ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);
            }
            catch (SocketException) {
                // Some of the socket's options may not be supported everywhere yet.
                Log.Entry(Priority.Warning, "Setting socket's keep alive option is not supported on this platform, ignoring.");
            }

            try {
                ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            }
            catch (SocketException) {
                // Unfortunately, setting lingering options causes this exception on Debian.
                // Just ignoring it.
                Log.Entry(Priority.Warning, "Setting socket's lingering options is not supported on this platform, ignoring.");
            }
        }

        protected TcpThreadedServer(int port)
            : this(IPAddress.Any, port) { }

        #endregion

        #region Abstract Methods

        protected abstract void HandleClient(Socket socket);
        protected abstract void HandleError(Socket socket, Exception e);

        #endregion

        #region ThreadedServer Impl

        protected override void OnStart() {
            try {
                ListenerSocket.Bind(LocalEndPoint);
                ListenerSocket.Listen(500);

                LocalEndPoint = (IPEndPoint) ListenerSocket.LocalEndPoint;
            }
            catch (SocketException e) {
                if (e.SocketErrorCode == SocketError.AddressAlreadyInUse) {
                    throw new InvalidOperationException("Specified address/port is already in use.");
                }
            }
            catch (ObjectDisposedException) {
                /* OnStop was called and socket got disposed. */
            }

            Log.Entry(Priority.Info, $"TCP server `{ServerName}` started on port `{Port}`.");
        }

        protected override void OnStop() {
            ListenerSocket.Dispose();
            Log.Entry($"Server `{ServerName}` stopped on port `{Port}`.");
        }

        protected override object AcceptConnection() {
            try {
                var s = ListenerSocket.Accept();

                if (ClientSocketReceiveTimeout != null) {
                    s.ReceiveTimeout = ClientSocketReceiveTimeout.Value;
                }
                if (ClientSocketSendTimeout != null) {
                    s.SendTimeout = ClientSocketSendTimeout.Value;
                }
                if (ClientSocketBufferSize != null) {
                    s.ReceiveBufferSize = s.SendBufferSize = ClientSocketBufferSize.Value;
                }

                try {
                    s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, KeepAlive);
                }
                catch (SocketException) {
                    Log.Entry(Priority.Warning, "Setting socket's keep alive options is not supported on this platform, ignoring.");
                }

                try {
                    s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, DontLinger);
                }
                catch (SocketException) {
                    Log.Entry(Priority.Warning, "Setting socket's lingering options is not supported on this platform, ignoring.");
                }

                return s;
            }
            catch (SocketException e) {
                /* Ignore error on blocking call (`Accept`) cancellation. */
                if (e.SocketErrorCode != SocketError.Interrupted) {
                    throw;
                }
            }

            return null;
        }

        protected override void HandleConnection(object obj) {
            using (var s = (Socket) obj) {
                if (!s.IsReallyConnected()) {
                    return;
                }

                try {
                    HandleClient(s);
                }
                catch (Exception e) {
                    Log.Entry(Priority.Warning, $"TCP handler exception: {e}");
                    HandleError(s, e);
                }
                finally {
                    s.GracefulDisconnect();
                }
            }
        }
        
        #endregion
    }
}
