using System;
using System.Collections.Generic;
using System.Threading;
using Jettify.Logging;

namespace Jettify.Serve {
    public abstract class ThreadedServer
        : IServer {

        #region Private Fields

        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly ManualResetEventSlim _evt = new ManualResetEventSlim();

        private Thread _listenerThread;

        private int _started;
        private int _stopped;

        protected bool IsCancellationRequested => _cts.IsCancellationRequested;

        #endregion

        #region Abstract Methods

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract object AcceptConnection();
        protected abstract void HandleConnection(object obj);

        #endregion

        #region Self Methods

        private void Listen() {
            while (true) {
                if (_cts.IsCancellationRequested) {
                    return;
                }

                try {
                    var obj = AcceptConnection();

                    if (obj == null) {
                        continue;
                    }

                    ThreadPool.QueueUserWorkItem(HandleConnection, obj);
                }
                catch (ObjectDisposedException) {
                    return;
                }
                catch (Exception e) {
                    Log.Entry(Priority.Warning, "Accept client and job queue exception: {0}", e);
                }
            }
        }

        #endregion

        #region IServer API

        public string ServerName { get; protected set; }

        public void Start() {
            if (Interlocked.CompareExchange(ref _started, 1, 0) != 0) {
                throw new InvalidOperationException("Server already started.");
            }

            OnStart();

            _listenerThread = new Thread(Listen) {IsBackground = true, Name = "Server Listener Thread"};
            _listenerThread.Start();
            _evt.Reset();
        }

        public void Stop() {
            if (Interlocked.CompareExchange(ref _started, 0, 1) != 1) {
                throw new InvalidOperationException("Server was not started.");
            }

            try {
                _cts.Cancel();
                OnStop();
            }
            catch (Exception e) {
                Log.Entry(Priority.Warning, "Server stop exception: {0}", e);
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
