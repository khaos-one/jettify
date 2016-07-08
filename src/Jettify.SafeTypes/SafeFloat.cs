using System;
using System.Globalization;
using System.Runtime.Serialization;
using Jettify.Crypto;

namespace Jettify.SafeTypes {
    /// <summary>
    /// Safe from memory hacking float type.
    /// </summary>
    [DataContract]
    public struct SafeFloat
        : //IDisposable, 
            IEquatable<SafeFloat>,
            IComparable<SafeFloat>,
            IEquatable<float>,
            IComparable<float>,
            IFormattable {

        // Values are needed to be made public for protobuf-net dll serializator to work properly.

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly float _salt;

        /// <summary>(Needed for serialization, do not use this field)</summary>
        [DataMember(IsRequired = true)] private readonly float _storage;

        /// <summary>
        /// Creates new instance of the safe float with some given value.
        /// </summary>
        /// <param name="value">The actual value of the variable.</param>
        public SafeFloat(float value = 0) {
            _salt = RandomProvider.SmoothFloat;
            unchecked {
                _storage = value + _salt;
            }
        }

        /// <summary>
        /// Get the actual value of the float.
        /// </summary>
        public float Value {
            get {
                unchecked {
                    return _storage - (float) _salt;
                }
            }
        }

        /// <summary>
        /// Implicit <see cref="SafeFloat"/> -> <see cref="float"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="SafeFloat"/> value to convert.</param>
        public static explicit operator float(SafeFloat val) {
            return val.Value;
        }

        /// <summary>
        /// Implicit <see cref="float"/> -> <see cref="SafeFloat"/> conversion operator.
        /// </summary>
        /// <param name="val"><see cref="float"/> value to convert.</param>
        public static explicit operator SafeFloat(float val) {
            return new SafeFloat(val);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of sum of the arguments.</returns>
        public static SafeFloat operator +(SafeFloat lhs, SafeFloat rhs) {
            return new SafeFloat(lhs.Value + rhs.Value);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of sum of the arguments.</returns>
        public static SafeFloat operator +(SafeFloat lhs, float rhs) {
            return new SafeFloat(lhs.Value + rhs);
        }

        /// <summary>
        /// An addition operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of sum of the arguments.</returns>
        public static SafeFloat operator +(float lhs, SafeFloat rhs) {
            return new SafeFloat(lhs + rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of subtraction of the arguments.</returns>
        public static SafeFloat operator -(SafeFloat lhs, SafeFloat rhs) {
            return new SafeFloat(lhs.Value - rhs.Value);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of subtraction of the arguments.</returns>
        public static SafeFloat operator -(SafeFloat lhs, float rhs) {
            return new SafeFloat(lhs.Value - rhs);
        }

        /// <summary>
        /// A subtraction operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of subtraction of the arguments.</returns>
        public static SafeFloat operator -(float lhs, SafeFloat rhs) {
            return new SafeFloat(lhs + rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of multiplication of the arguments.</returns>
        public static SafeFloat operator *(SafeFloat lhs, SafeFloat rhs) {
            return new SafeFloat(lhs.Value*rhs.Value);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of multiplication of the arguments.</returns>
        public static SafeFloat operator *(SafeFloat lhs, float rhs) {
            return new SafeFloat(lhs.Value*rhs);
        }

        /// <summary>
        /// A multiplication operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of multiplication of the arguments.</returns>
        public static SafeFloat operator *(float lhs, SafeFloat rhs) {
            return new SafeFloat(lhs*rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of division of the arguments.</returns>
        public static SafeFloat operator /(SafeFloat lhs, SafeFloat rhs) {
            return new SafeFloat(lhs.Value/rhs.Value);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of division of the arguments.</returns>
        public static SafeFloat operator /(SafeFloat lhs, float rhs) {
            return new SafeFloat(lhs.Value/rhs);
        }

        /// <summary>
        /// A division operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>New <see cref="SafeFloat"/> with the value of division of the arguments.</returns>
        public static SafeFloat operator /(float lhs, SafeFloat rhs) {
            return new SafeFloat(lhs/rhs.Value);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeFloat lhs, SafeFloat rhs) {
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(SafeFloat lhs, float rhs) {
            return lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An equality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are equal or not.</returns>
        public static bool operator ==(float lhs, SafeFloat rhs) {
            return lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeFloat lhs, SafeFloat rhs) {
            return !lhs.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(SafeFloat lhs, float rhs) {
            return !lhs.Value.Equals(rhs);
        }

        /// <summary>
        /// An inequality operator.
        /// </summary>
        /// <param name="lhs">Right hand side argument.</param>
        /// <param name="rhs">Left hand side argument.</param>
        /// <returns>Whether the values are inequal or not.</returns>
        public static bool operator !=(float lhs, SafeFloat rhs) {
            return !lhs.Equals(rhs.Value);
        }

        /// <summary>
        /// An increment operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeFloat"/> with the incremented value.</returns>
        public static SafeFloat operator ++(SafeFloat val) {
            return new SafeFloat(val.Value + 1);
        }

        /// <summary>
        /// An decrement operator.
        /// </summary>
        /// <param name="val">Current value.</param>
        /// <returns>New <see cref="SafeFloat"/> with the decremented value.</returns>
        public static SafeFloat operator --(SafeFloat val) {
            return new SafeFloat(val.Value - 1);
        }

        //public void Dispose() {
        //    _salt = _storage = 0f;
        //}

        public bool Equals(SafeFloat other) {
            return Value.Equals(other.Value);
        }

        public int CompareTo(SafeFloat other) {
            return Value.CompareTo(other.Value);
        }

        public bool Equals(float other) {
            return Value.Equals(other);
        }

        public int CompareTo(float other) {
            return Value.CompareTo(other);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SafeFloat && Equals((SafeFloat) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_salt.GetHashCode()*397) ^ _storage.GetHashCode();
            }
        }

        public override string ToString() {
            return Value.ToString(CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider) {
            return Value.ToString(format, formatProvider);
        }
    }
}
