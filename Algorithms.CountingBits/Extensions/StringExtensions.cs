using System;

namespace Algorithms.CountingBits.Extensions
{
    static class StringExtensions
    {

        #region Public Methods

        public static string Reverse(this string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        #endregion
    }

}
