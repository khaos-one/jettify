using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extensions for <see cref="Socket"/> objects.
    /// </summary>
    public static class SocketExtensions {
        public const int MaxUdpDatagramSize = 0x10000;

        public static NetworkStream GetStream(this Socket s, bool ownsSocket = false) {
            return new NetworkStream(s, ownsSocket);
        }

        public static int PollRead(this Socket s, byte[] buffer, int microsecondsTimeout) {
            if (s.Poll(microsecondsTimeout, SelectMode.SelectRead)) {
                return s.Receive(buffer);
            }

            return -1;
        }

        public static bool IsReallyConnected(this Socket s, int timeout = 100) {
            return !((s.Poll(timeout, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }
    }
}
