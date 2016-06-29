using System.Text;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extension methods for <see cref="string"/> objects.
    /// </summary>
    public static class StringExtensions {
        public static string Format(this string s, params object[] args) {
            return string.Format(s, args);
        }

        public static byte[] ToBytes(this string s, Encoding e) {
            return e.GetBytes(s);
        }
    }
}
