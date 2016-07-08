using System;
using System.Runtime.Serialization;
using Jettify.Crypto;

namespace Jettify.SafeTypes {
    /// <summary>
    /// Safe from memory hacking UInt32.
    /// </summary>
    [DataContract]
    public struct SafeUInt
        : //IDisposable, 
            IEquatable<SafeUInt>,
            IComparable<SafeUInt>,
            IEquatable<uint>,
            IComparable<uint>,
            IFormattable {

        // Values are needed to be made public for protobuf-net dll serializator to work properly.

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly uint _salt;

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly uint _storage;

        /// <summary>
        /// Creates new instance of the <see cref="SafeUInt"/> with some given value.
        /// </summary>
        /// <param name="value">The actual value of the variable.</param>
        public SafeUInt(uint value = 0) {
            _salt = RandomProvider.UInt32;

            unchecked {
                _storage = value + _salt;
            }
        }

        /// <summary>
        /// Get the actual value of the float.
        /// </summary>
        public uint Value {
            get {
                unchecked {
                    return _storage - _salt;
                }
            }
        }

        /// <summary>
        /// Implicit <see cref="SafeUInt"/> -> <see cref="uint"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static implicit operator uint(SafeUInt val) {
            return val.Value;
        }

        /// <summary>
        /// Implicit <see cref="uint"/> -> <see cref="SafeUInt"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static implicit operator SafeUInt(uint val) {
            return new SafeUInt(val);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of sum of the arguments.</returns>
        public static SafeUInt operator +(SafeUInt lhs, SafeUInt rhs) {
            return new SafeUInt(lhs.Value + rhs.Value);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of sum of the arguments.</returns>
        public static SafeUInt operator +(SafeUInt lhs, uint rhs) {
            return new SafeUInt(lhs.Value + rhs);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of sum of the arguments.</returns>
        public static SafeUInt operator +(uint lhs, SafeUInt rhs) {
            return new SafeUInt(lhs + rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeUInt operator -(SafeUInt lhs, SafeUInt rhs) {
            return new SafeUInt(lhs.Value - rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeUInt operator -(SafeUInt lhs, uint rhs) {
            return new SafeUInt(lhs.Value - rhs);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeUInt operator -(uint lhs, SafeUInt rhs) {
            return new SafeUInt(lhs + rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeUInt operator *(SafeUInt lhs, SafeUInt rhs) {
            return new SafeUInt(lhs.Value*rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeUInt operator *(SafeUInt lhs, uint rhs) {
            return new SafeUInt(lhs.Value*rhs);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeUInt operator *(uint lhs, SafeUInt rhs) {
            return new SafeUInt(lhs*rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of division of the arguments.</returns>
        public static SafeUInt operator /(SafeUInt lhs, SafeUInt rhs) {
            return new SafeUInt(lhs.Value/rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of division of the arguments.</returns>
        public static SafeUInt operator /(SafeUInt lhs, uint rhs) {
            return new SafeUInt(lhs.Value/rhs);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeUInt"/> with the value of division of the arguments.</returns>
        public static SafeUInt operator /(uint lhs, SafeUInt rhs) {
            return new SafeUInt(lhs/rhs.Value);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeUInt lhs, SafeUInt rhs) {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeUInt lhs, uint rhs) {
            return lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(uint lhs, SafeUInt rhs) {
            return lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeUInt lhs, SafeUInt rhs) {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeUInt lhs, uint rhs) {
            return !lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(uint lhs, SafeUInt rhs) {
            return !lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeUInt"/> with the incremented value.</returns>
        public static SafeUInt operator ++(SafeUInt val) {
            return new SafeUInt(val.Value + 1);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeUInt"/> with the incremented value.</returns>
        public static SafeUInt operator --(SafeUInt val) {
            return new SafeUInt(val.Value - 1);
        }

        //public void Dispose() {
        //    _salt = _storage = 0;
        //}

        public bool Equals(SafeUInt other) {
            return Value.Equals(other.Value);
        }

        public int CompareTo(SafeUInt other) {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(uint other) {
            return Value.Equals(other);
        }

        public int CompareTo(uint other) {
            return Value.CompareTo(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SafeUInt && Equals((SafeUInt) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((int) _salt*397) ^ (int) _storage;
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
