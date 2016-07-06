using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Jettify.Logging;

namespace Jettify.Serve {
    public class AsyncTcpServer 
        : IServer {

        #region Self Fields & Props

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ManualResetEventSlim _evt = new ManualResetEventSlim();
        private int _started;

        protected Socket ListenerSocket;
        protected Thread WorkerThread;

        public int? ClientSendTimeout { get; protected set; }
        public int? ClientReceiveTimeout { get; protected set; }
        public int? ClientBufferSize { get; protected set; }
        public bool KeepAlive { get; protected set; }
        public bool DontLinger { get; protected set; }

        public IPEndPoint LocalEndPoint { get; protected set; }
        public int Port => LocalEndPoint.Port;

        #endregion

        #region Constructors

        public AsyncTcpServer(IPAddress ip, int port, int? sendTimeout = null, int? receiveTimeout = null,
            int? bufferSize = null, bool keepAlive = false, bool dontLinger = true, string serverName = "AsyncTcpServer") {
            LocalEndPoint = new IPEndPoint(ip, port);
            ClientSendTimeout = sendTimeout;
            ClientReceiveTimeout = receiveTimeout;
            ClientBufferSize = bufferSize;
            KeepAlive = keepAlive;
            DontLinger = dontLinger;
            ServerName = serverName;

            ListenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, false);
            ListenerSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
        }

        public AsyncTcpServer(int port, int? sendTimeout = null, int? receiveTimeout = null,
            int? bufferSize = null, bool keepAlive = false, bool dontLinger = true, string serverName = "AsyncTcpServer")
            : this(IPAddress.Any, port, sendTimeout, receiveTimeout, bufferSize, keepAlive, dontLinger, serverName) { }

        #endregion

        #region Virtual Methods

        #pragma warning disable 1998
        protected virtual async Task HandleClient(Socket s, CancellationToken ct) { }
        #pragma warning restore 1998

        #endregion

        #region Self Methods

        protected async Task Run(CancellationToken ct) {
            while (true) {
                if (ct.IsCancellationRequested) {
                    return;
                }

                using (var s = await ListenerSocket.AcceptAsync()) {
                    try {
                        await HandleClient(s, ct);
                    }
                    catch (OperationCanceledException) {}
                    catch (Exception e) {
                        Log.Entry(Priority.Warning, $"Server `{ServerName}` client handling exception: {e}");
                    }
                }
            }
        }

        #endregion

        #region IServer Impl

        public string ServerName { get; }

        public void Start() {
            if (Interlocked.CompareExchange(ref _started, 1, 0) != 0) {
                throw new InvalidOperationException("Server already started.");
            }

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
                /* Do nothing -- the object was disposed before initialize. */
            }

            _evt.Reset();

            #pragma warning disable 4014
            try {
                Run(_cts.Token);
            }
            catch (OperationCanceledException) { }
            #pragma warning restore 4014

            Log.Entry(Priority.Info, $"Server `{ServerName}` started on port `{Port}`.");
        }

        public void Stop() {
            if (Interlocked.CompareExchange(ref _started, 0, 1) != 1) {
                throw new InvalidOperationException("Server was not started.");
            }

            try {
                _cts.Cancel();
            }
            catch (Exception e) {
                Log.Entry(Priority.Warning, $"Server `{ServerName}` stop exception: {e}");
            }
            finally {
                _evt.Set();
            }
        }

        public void WaitForJoin(int millisecondsTimeout = -1) {
            _evt.Wait(millisecondsTimeout);
        }

        public void Dispose() {
            Stop();
            WaitForJoin();
        }

        #endregion
    }
}
