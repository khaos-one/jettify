﻿using System;

namespace Jettify.Crypto {
    public static class RandomProvider {
        private static readonly Random _random = new Random();

        public static byte[] GetBytes(int length) {
            var buffer = new byte[length];
            _random.NextBytes(buffer);

            return buffer;
        }

        public static ulong GetUInt64() {
            return BitConverter.ToUInt64(GetBytes(sizeof (ulong)), 0);
        }

        public static uint GetUInt32() {
            return BitConverter.ToUInt32(GetBytes(sizeof (uint)), 0);
        }

        public static ushort GetUInt16() {
            return BitConverter.ToUInt16(GetBytes(sizeof (ushort)), 0);
        }

        public static long GetInt64() {
            return BitConverter.ToInt64(GetBytes(sizeof (long)), 0);
        }

        public static int GetInt32() {
            return BitConverter.ToInt32(GetBytes(sizeof (int)), 0);
        }

        public static short GetInt16() {
            return BitConverter.ToInt16(GetBytes(sizeof (short)), 0);
        }

        public static double GetDouble() {
            return BitConverter.ToDouble(GetBytes(sizeof (double)), 0);
        }

        public static float GetFloat() {
            return BitConverter.ToSingle(GetBytes(sizeof (float)), 0);
        }

        public static float GetSmoothFloat() {
            var mantissa = (_random.NextDouble()*2.0) - 1.0;
            var exponent = Math.Pow(2.0, _random.Next(-126, 128));
            return (float) (mantissa*exponent);
        }
        
        public static string GetString(int length) {
            // TODO: Method might need result validation along with `SafeString`.
            var res = new byte[length*sizeof (char)];
            _random.NextBytes(res);
            var chars = new char[length];

            for (var i = 0; i < chars.Length; i++) {
                chars[i] = (char) res[i*sizeof (char)];
            }

            return new string(chars);
        }

        public static ulong UInt64 => GetUInt64();
        public static uint UInt32 => GetUInt32();
        public static ushort UInt16 => GetUInt16();
        public static long Int64 => GetInt64();
        public static int Int32 => GetInt32();
        public static short Int16 => GetInt16();
        public static double Double => GetDouble();
        public static float Float => GetFloat();
        public static float SmoothFloat => GetSmoothFloat();
    }
}
