using System;
using System.Runtime.Serialization;
using Jettify.Crypto;

namespace Jettify.SafeTypes {
    /// <summary>
    /// Safe from memory hacking Int32.
    /// </summary>
    [DataContract]
    public struct SafeInt
        : //IDisposable, 
            IEquatable<SafeInt>,
            IComparable<SafeInt>,
            IEquatable<int>,
            IComparable<int>,
            IFormattable {

        // Values are needed to be made public for protobuf-net dll serializator to work properly.

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly int _salt;

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly int _storage;

        /// <summary>
        /// Creates new instance of the <see cref="SafeInt"/> with some given value.
        /// </summary>
        /// <param name="value">The actual value of the variable.</param>
        public SafeInt(int value = 0) {
            _salt = RandomProvider.Int32;

            unchecked {
                _storage = value + _salt;
            }
        }

        /// <summary>
        /// Get the actual value of the float.
        /// </summary>
        public int Value {
            get {
                unchecked {
                    return _storage - _salt;
                }
            }
        }

        /// <summary>
        /// Implicit <see cref="SafeInt"/> -> <see cref="int"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static explicit operator int(SafeInt val) {
            return val.Value;
        }

        /// <summary>
        /// Implicit <see cref="int"/> -> <see cref="SafeInt"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static explicit operator SafeInt(int val) {
            return new SafeInt(val);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of sum of the arguments.</returns>
        public static SafeInt operator +(SafeInt lhs, SafeInt rhs) {
            return new SafeInt(lhs.Value + rhs.Value);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of sum of the arguments.</returns>
        public static SafeInt operator +(SafeInt lhs, int rhs) {
            return new SafeInt(lhs.Value + rhs);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of sum of the arguments.</returns>
        public static SafeInt operator +(int lhs, SafeInt rhs) {
            return new SafeInt(lhs + rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeInt operator -(SafeInt lhs, SafeInt rhs) {
            return new SafeInt(lhs.Value - rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeInt operator -(SafeInt lhs, int rhs) {
            return new SafeInt(lhs.Value - rhs);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of subtraction of the arguments.</returns>
        public static SafeInt operator -(int lhs, SafeInt rhs) {
            return new SafeInt(lhs + rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeInt operator *(SafeInt lhs, SafeInt rhs) {
            return new SafeInt(lhs.Value*rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeInt operator *(SafeInt lhs, int rhs) {
            return new SafeInt(lhs.Value*rhs);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of multiplication of the arguments.</returns>
        public static SafeInt operator *(int lhs, SafeInt rhs) {
            return new SafeInt(lhs*rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of division of the arguments.</returns>
        public static SafeInt operator /(SafeInt lhs, SafeInt rhs) {
            return new SafeInt(lhs.Value/rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of division of the arguments.</returns>
        public static SafeInt operator /(SafeInt lhs, int rhs) {
            return new SafeInt(lhs.Value/rhs);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeInt"/> with the value of division of the arguments.</returns>
        public static SafeInt operator /(int lhs, SafeInt rhs) {
            return new SafeInt(lhs/rhs.Value);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeInt lhs, SafeInt rhs) {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeInt lhs, int rhs) {
            return lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(int lhs, SafeInt rhs) {
            return lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeInt lhs, SafeInt rhs) {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeInt lhs, int rhs) {
            return !lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(int lhs, SafeInt rhs) {
            return !lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeInt"/> with the incremented value.</returns>
        public static SafeInt operator ++(SafeInt val) {
            return new SafeInt(val.Value + 1);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeInt"/> with the incremented value.</returns>
        public static SafeInt operator --(SafeInt val) {
            return new SafeInt(val.Value - 1);
        }

        //public void Dispose() {
        //    _salt = _storage = 0;
        //}

        public bool Equals(SafeInt other) {
            return Value.Equals(other.Value);
        }

        public int CompareTo(SafeInt other) {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(int other) {
            return Value.Equals(other);
        }

        public int CompareTo(int other) {
            return Value.CompareTo(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SafeInt && Equals((SafeInt) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_storage*397) ^ _storage;
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
