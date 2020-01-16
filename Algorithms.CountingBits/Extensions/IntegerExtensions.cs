using System;

namespace Algorithms.CountingBits.Extensions
{
    static class IntegerExtensions
    {
        private const int DECIMAL_BASE = 2;

        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, DECIMAL_BASE);
        }
    }
}
