using System;

namespace Algorithms.CountingBits.Extensions
{
    static class IntegerExtensions
    {
        #region Consts

        private const int DECIMAL_BASE = 2;

        #endregion

        #region Public Methods

        public static string ToBinary(this int value)
        {
            return Convert.ToString(value, DECIMAL_BASE);
        }

        #endregion
    }
}
