using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extensions for <see cref="Stream"/> objects.
    /// </summary>
    public static class StreamExtensions {
        public static void Write(this Stream stream, byte[] bytes, int offset = 0) {
            stream.Write(bytes, offset, bytes.Length);
        }

        public static async Task WriteAsync(this Stream stream, byte[] bytes, int offset = 0) {
            await stream.WriteAsync(bytes, offset, bytes.Length);
        }

        public static async Task WriteAsync(this Stream stream, byte[] bytes, CancellationToken token, int offset = 0) {
            if (!token.IsCancellationRequested) {
                await stream.WriteAsync(bytes, offset, bytes.Length, token);
            }
        }

        public static void Write(this Stream stream, string s, Encoding encoding) {
            var bytes = encoding.GetBytes(s);
            stream.Write(bytes);
        }

        public static async Task WriteAsync(this Stream stream, string s, Encoding encoding) {
            var bytes = encoding.GetBytes(s);
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        public static async Task WriteAsync(this Stream stream, string s, Encoding encoding, CancellationToken token) {
            if (!token.IsCancellationRequested) {
                var bytes = encoding.GetBytes(s);
                await stream.WriteAsync(bytes, 0, bytes.Length, token);
            }
        }

        public static int Read(this Stream stream, byte[] bytes, int offset = 0) {
            return stream.Read(bytes, offset, bytes.Length - offset);
        }

        public static async Task<int> ReadAsync(this Stream stream, byte[] bytes, int offset = 0) {
            return await stream.ReadAsync(bytes, offset, bytes.Length - offset);
        }

        public static async Task<int> ReadAsync(this Stream stream, byte[] bytes, CancellationToken token, int offset = 0) {
            if (!token.IsCancellationRequested) {
                return await stream.ReadAsync(bytes, offset, bytes.Length - offset);
            }

            return -1;
        }

        public static byte[] ReadBytes(this Stream stream, int count, int offset = 0) {
            var buffer = new byte[count];
            stream.Read(buffer, offset, count);
            return buffer;
        }

        public static async Task<byte[]> ReadBytesAsync(this Stream stream, int count, int offset = 0) {
            var buffer = new byte[count];
            await stream.ReadAsync(buffer, count, offset);
            return buffer;
        }

        public static async Task<byte[]> ReadBytesAsync(this Stream stream, int count, CancellationToken token, int offset = 0) {
            if (!token.IsCancellationRequested) {
                var buffer = new byte[count];
                await stream.ReadAsync(buffer, count, offset, token);
                return buffer;
            }

            return null;
        }

        public static string ReadString(this Stream stream, Encoding encoding) {
            using (var sr = new StreamReader(stream, encoding)) {
                return sr.ReadToEnd();
            }
        }
    }
}
