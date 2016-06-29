using System;

namespace Jettify.Extensions {
    /// <summary>
    /// Provides extensions for <see cref="float"/> objects.
    /// </summary>
    public static class FloatExtensions {
        public static bool IsNear(this float val, float otherVal, float epsilon = 0.01f) {
            return Math.Abs(val - otherVal) <= epsilon;
        }

        public static bool IsBetween(this float val, float a, float b) {
            return a < val && val < b;
        }

        public static bool IsBetweenInclusive(this float val, float a, float b) {
            return a <= val && val <= b;
        }
    }
}
