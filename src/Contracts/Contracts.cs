using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Contracts
{
    public static class Contracts
    {
        #region Debug

        private const string Debug = "DEBUG";

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void LessThanDebug<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) >= 0)
            {
                throw new ContractOutOfRangeException("a >= b");
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void BiggerOrEqualThanDebug<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) < 0)
            {
                throw new ContractOutOfRangeException("a < b");
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void LengthEqualsDebug(this string s, int length)
        {
            int count = string.IsNullOrEmpty(s) ? 0 : s.Length;

            if (length != count) throw new ContractOutOfRangeException("Length != " + count);
            
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void LengthEqualsDebug<T>(this ICollection<T> data, int length)
        {
            int count = data.Count;

            if (length != count) throw new ContractOutOfRangeException("Length != " + count);

        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void NotEqualsDebug<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) != 0)
            {
                throw new ContractNotEqualsException("a != b");
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void NotNullDebug<T>(this T a) where T : class
        {
            if (a == null)
            {
                throw new ContractNotNullException("a");
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void ThrowIfNullDebug<T>(this T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void ThrowIfNullDebug<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(typeof(T).Name.ToLowerInvariant());
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void ThrowIfEmptyDebug(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument cannot be null or an empty string.","value");
            }
        }

        [DebuggerHidden]
        [Conditional(Debug)]
        public static void ThrowIfElementNullDebug<T>(this IEnumerable<T> array) where T : class
        {
            array.ThrowIfNull();

            foreach (T item in array)
            {
                if (item == null)
                {
                    throw new ArgumentNullException(String.Format(CultureInfo.InvariantCulture, "Collection {0} contains an empty element.", array.GetType().Name));
                }
            }
        }

        #endregion

        #region Normal

        [DebuggerHidden]
        public static void LessThan<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) >= 0)
            {
                throw new ContractOutOfRangeException("a >= b");
            }
        }

        [DebuggerHidden]
        public static void BiggerOrEqualThan<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) < 0)
            {
                throw new ContractOutOfRangeException("a < b");
            }
        }

        [DebuggerHidden]
        public static void LengthEquals(this string s, int length)
        {
            int count = string.IsNullOrEmpty(s) ? 0 : s.Length;

            if (length != count) throw new ContractOutOfRangeException("Length != " + count);

        }

        [DebuggerHidden]
        public static void LengthEquals<T>(this ICollection<T> data, int length)
        {
            int count = data.Count;

            if (length != count) throw new ContractOutOfRangeException("Length != " + count);

        }

        [DebuggerHidden]
        public static void NotEquals<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) != 0)
            {
                throw new ContractNotEqualsException("a != b");
            }
        }

        [DebuggerHidden]
        public static void NotNull<T>(this T a) where T : class
        {
            if (a == null)
            {
                throw new ContractNotNullException("a");
            }
        }

        [DebuggerHidden]
        public static void ThrowIfNull<T>(this T value, string name) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        [DebuggerHidden]
        public static void ThrowIfEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Argument cannot be null or an empty string.", "value");
            }
        }

        [DebuggerHidden]
        public static void ThrowIfNull<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(typeof(T).Name.ToLowerInvariant());
            }
        }

        [DebuggerHidden]
        public static void ThrowIfElementNull<T>(this IEnumerable<T> array) where T : class
        {
            array.ThrowIfNull();

            foreach (T item in array)
            {
                if (item == null)
                {
                    throw new ArgumentNullException(String.Format(CultureInfo.InvariantCulture, "Collection {0} contains an empty element.", array.GetType().Name));
                }
            }
        }

        #endregion

    }
}