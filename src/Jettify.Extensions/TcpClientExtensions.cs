using System;
using System.Net.Sockets;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extension methods for <see cref="TcpClient"/> objects.
    /// </summary>
    public static class TcpClientExtensions {
        public static void ConnectWithTimeout(this TcpClient client, string hostname, int port, int timeout = 5000) {
            var task = client.ConnectAsync(hostname, port);

            try {
                task.Wait(timeout);
            }
            catch (AggregateException e) {
                throw e.InnerException;
            }

            if (!task.IsCompleted) {
                throw new TimeoutException("Connect timeout.");
            }
        }
    }
}
