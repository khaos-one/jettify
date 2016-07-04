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

        private List<WaitHandle> _threadPoolHandles;

        protected bool IsCancellationRequested => _cts.IsCancellationRequested;

        #endregion

        #region Abstract Methods

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract object AcceptClient();
        protected abstract void HandleClient(object obj);

        #endregion

        #region Self Methods

        private void Listen() {
            while (true) {
                if (_cts.IsCancellationRequested) {
                    return;
                }

                try {
                    var obj = AcceptClient();

                    if (obj == null) {
                        continue;
                    }

                    ThreadPool.QueueUserWorkItem(HandleClient, obj);
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

        public void Dispose() {
            throw new System.NotImplementedException();
        }

        public void Start() {
            throw new System.NotImplementedException();
        }

        public void Stop() {
            throw new System.NotImplementedException();
        }

        public void WaitForJoin(int millisecondsTimeout = -1) {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
