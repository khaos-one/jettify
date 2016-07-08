using System;
using System.Runtime.Serialization;
using Jettify.Crypto;

namespace Jettify.SafeTypes {
    /// <summary>
    /// Safe from memory hacking Int32.
    /// </summary>
    [DataContract]
    public struct SafeLong
        : //IDisposable,
            IEquatable<SafeLong>,
            IComparable<SafeLong>,
            IEquatable<long>,
            IComparable<long>,
            IFormattable {

        // Values are needed to be made public for protobuf-net dll serializator to work properly.

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly long _salt;

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly long _storage;

        /// <summary>
        /// Creates new instance of the <see cref="SafeLong"/> with some given value.
        /// </summary>
        /// <param name="value">The actual value of the variable.</param>
        public SafeLong(long value = 0) {
            _salt = RandomProvider.Int64;

            unchecked {
                _storage = value + _salt;
            }
        }

        /// <summary>
        /// Get the actual value of the float.
        /// </summary>
        public long Value {
            get {
                unchecked {
                    return _storage - _salt;
                }
            }
        }

        /// <summary>
        /// Implicit <see cref="SafeLong"/> -> <see cref="long"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static implicit operator long(SafeLong val) {
            return val.Value;
        }

        /// <summary>
        /// Implicit <see cref="long"/> -> <see cref="SafeLong"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static implicit operator SafeLong(long val) {
            return new SafeLong(val);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of sum of the arguments.</returns>
        public static SafeLong operator +(SafeLong lhs, SafeLong rhs) {
            return new SafeLong(lhs.Value + rhs.Value);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of sum of the arguments.</returns>
        public static SafeLong operator +(SafeLong lhs, long rhs) {
            return new SafeLong(lhs.Value + rhs);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of sum of the arguments.</returns>
        public static SafeLong operator +(long lhs, SafeLong rhs) {
            return new SafeLong(lhs + rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of subtraction of the arguments.</returns>
        public static SafeLong operator -(SafeLong lhs, SafeLong rhs) {
            return new SafeLong(lhs.Value - rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of subtraction of the arguments.</returns>
        public static SafeLong operator -(SafeLong lhs, long rhs) {
            return new SafeLong(lhs.Value - rhs);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of subtraction of the arguments.</returns>
        public static SafeLong operator -(long lhs, SafeLong rhs) {
            return new SafeLong(lhs + rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of multiplication of the arguments.</returns>
        public static SafeLong operator *(SafeLong lhs, SafeLong rhs) {
            return new SafeLong(lhs.Value*rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of multiplication of the arguments.</returns>
        public static SafeLong operator *(SafeLong lhs, long rhs) {
            return new SafeLong(lhs.Value*rhs);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of multiplication of the arguments.</returns>
        public static SafeLong operator *(long lhs, SafeLong rhs) {
            return new SafeLong(lhs*rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of division of the arguments.</returns>
        public static SafeLong operator /(SafeLong lhs, SafeLong rhs) {
            return new SafeLong(lhs.Value/rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of division of the arguments.</returns>
        public static SafeLong operator /(SafeLong lhs, long rhs) {
            return new SafeLong(lhs.Value/rhs);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeLong"/> with the value of division of the arguments.</returns>
        public static SafeLong operator /(long lhs, SafeLong rhs) {
            return new SafeLong(lhs/rhs.Value);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeLong lhs, SafeLong rhs) {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeLong lhs, long rhs) {
            return lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(long lhs, SafeLong rhs) {
            return lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeLong lhs, SafeLong rhs) {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeLong lhs, long rhs) {
            return !lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(long lhs, SafeLong rhs) {
            return !lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeLong"/> with the incremented value.</returns>
        public static SafeLong operator ++(SafeLong val) {
            return new SafeLong(val.Value + 1);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeLong"/> with the incremented value.</returns>
        public static SafeLong operator --(SafeLong val) {
            return new SafeLong(val.Value - 1);
        }

        //public void Dispose() {
        //    _salt = _storage = 0;
        //}

        public bool Equals(SafeLong other) {
            return Value.Equals(other.Value);
        }

        public int CompareTo(SafeLong other) {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(long other) {
            return Value.Equals(other);
        }

        public int CompareTo(long other) {
            return Value.CompareTo(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SafeLong && Equals((SafeLong) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) _storage*397) ^ (int) _storage;
            }
        }

        public override string ToString() {
            return Value.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider) {
            return Value.ToString(format, formatProvider);
        }
    }
}
