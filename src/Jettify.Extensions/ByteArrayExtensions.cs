using System;
using System.Text;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extension methods for arrays of <see cref="byte"/>.
    /// </summary>
    public static class ByteArrayExtensions {
        public static string ToString(this byte[] a, Encoding e) {
            return e.GetString(a);
        }

        public static string ToString(this byte[] a, Encoding e, int offset) {
            return e.GetString(a, offset, a.Length - offset);
        }

        public static string ToString(this byte[] a, Encoding e, int offset, int length) {
            return e.GetString(a, offset, length);
        }

        public static void BlockCopyTo(this byte[] a, byte[] target, int sourceOffset = 0, int targetOffset = 0) {
            Buffer.BlockCopy(a, sourceOffset, target, targetOffset, a.Length);
        }

        public static void BlockCopyFrom(this byte[] a, byte[] target, int sourceOffset = 0, int targetOffset = 0) {
            Buffer.BlockCopy(target, targetOffset, a, sourceOffset, target.Length);
        }

        public static string ToHexString(this byte[] a) {
            unchecked {
                var c = new char[a.Length*2];

                for (int bx = 0, cx = 0; bx < a.Length; ++bx, ++cx) {
                    var b = ((byte) (a[bx] >> 4));
                    c[cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                    b = ((byte) (a[bx] & 0x0F));
                    c[++cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);
                }

                return new string(c);
            }
        }

        public static string ToHexString(this byte[] a, char delimeter) {
            unchecked {
                var l = a.Length*3;
                var c = new char[l];

                for (int bx = 0, cx = 0; bx < a.Length; ++bx, ++cx) {
                    var b = ((byte) (a[bx] >> 4));
                    c[cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                    b = ((byte) (a[bx] & 0x0F));
                    c[++cx] = (char) (b > 9 ? b + 0x37 + 0x20 : b + 0x30);

                    c[++cx] = delimeter;
                }

                return new string(c, 0, l - 1);
            }
        }
    }
}
