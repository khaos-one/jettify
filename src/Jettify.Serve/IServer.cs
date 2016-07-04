using System;

namespace Jettify.Serve {
    public interface IServer
        : IDisposable {

        string ServerName { get; }

        void Start();
        void Stop();
        void WaitForJoin(int millisecondsTimeout = -1);
    }
}
