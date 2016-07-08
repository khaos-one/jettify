using System;
using System.Runtime.Serialization;
using Jettify.Crypto;

namespace Jettify.SafeTypes {
    /// <summary>
    /// Safe from memory hacking <see cref="string"/>.
    /// </summary>
    [DataContract]
    public sealed class SafeString
        : //IDisposable,
            IEquatable<SafeString>,
            IComparable<SafeString>,
            IEquatable<string>,
            IComparable<string> {

        // Values are needed to be made public for protobuf-net dll serializator to work properly.
        // TODO: Class might need result validation.

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly string _salt;

        /// <summary>(Needed for serialization, do not use this field)</summary>

        [DataMember(IsRequired = true)] private readonly string _storage;

        // Empty constructor is meaningless but needed for serializer.

        /// <summary>
        /// Creates new instance of <see cref="SafeString"/>.
        /// </summary>
        public SafeString() {}

        /// <summary>
        /// Creates new instance of <see cref="SafeString"/>
        /// </summary>
        /// <param name="str">Original string value.</param>
        public SafeString(string str) {
            _salt = RandomProvider.GetString(str.Length);
            _storage = PlusString(str, _salt);
        }

        private static string PlusString(string a, string b) {
            unchecked {
                if (a.Length != b.Length) {
                    throw new ArgumentOutOfRangeException(nameof(b));
                }

                var chars = new char[a.Length];

                for (var i = 0; i < chars.Length; i++) {
                    chars[i] = (char) (a[i] + b[i]);
                }

                return new string(chars);
            }
        }

        private static string MinusString(string a, string b) {
            unchecked {
                if (a.Length != b.Length) {
                    throw new ArgumentOutOfRangeException(nameof(b));
                }

                var chars = new char[a.Length];

                for (var i = 0; i < chars.Length; i++) {
                    chars[i] = (char) (a[i] - b[i]);
                }

                return new string(chars);
            }
        }

        /// <summary>
        /// The original value.
        /// </summary>
        public string Value => MinusString(_storage, _salt);

        /// <summary>
        /// Implicit <see cref="SafeString"/> -> <see cref="string"/> conversion operator.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        public static implicit operator string(SafeString val) {
            return val.Value;
        }

        /// <summary>
        /// Implicit <see cref="string"/> -> <see cref="SafeString"/> conversion operator.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        public static implicit operator SafeString(string val) {
            return new SafeString(val);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are equal or not.</returns>
        public static bool operator ==(SafeString lhs, SafeString rhs) {
            return !ReferenceEquals(lhs, null) && lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are equal or not.</returns>
        public static bool operator ==(SafeString lhs, string rhs) {
            return !ReferenceEquals(lhs, null) && lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are equal or not.</returns>
        public static bool operator ==(string lhs, SafeString rhs) {
            return lhs != null && !ReferenceEquals(rhs, null) && rhs.Equals(lhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are inequal or not.</returns>
        public static bool operator !=(SafeString lhs, SafeString rhs) {
            return !(lhs == rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are inequal or not.</returns>
        public static bool operator !=(SafeString lhs, string rhs) {
            return !(lhs == rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether values are inequal or not.</returns>
        public static bool operator !=(string lhs, SafeString rhs) {
            return !(lhs == rhs);
        }

        //public void Dispose() {
        //    _salt = _storage = null;
        //}

        public bool Equals(SafeString other) {
            if (ReferenceEquals(this, other)) {
                return true;
            }

            return !ReferenceEquals(other, null) && Value.Equals(other.Value);
        }

        public int CompareTo(SafeString other) {
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        public bool Equals(string other) {
            return !ReferenceEquals(other, null) && Value.Equals(other);
        }

        public int CompareTo(string other) {
            return string.Compare(Value, other, StringComparison.Ordinal);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is SafeString && Equals((SafeString) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((_salt != null ? _salt.GetHashCode() : 0)*397) ^ (_storage != null ? _storage.GetHashCode() : 0);
            }
        }

        public override string ToString() {
            return Value;
        }
    }
}
