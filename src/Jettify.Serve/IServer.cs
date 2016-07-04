using System;

namespace Jettify.Serve {
    public interface IServer
        : IDisposable {

        void Start();
        void Stop();
        void WaitForJoin(int millisecondsTimeout = -1);
    }
}
