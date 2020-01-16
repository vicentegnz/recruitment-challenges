using Algorithms.CountingBits.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Algorithms.CountingBits.Infraestructure
{
    public class PositiveBitCounterService : IPositiveBitCounterService
    {
        #region Public Methods

        public List<int> GetAllIndexesOfPositiveBitMatches(string input, string match)
        {
            return Regex.Matches(input, match).Select(x => x.Index).ToList();
        }

        #endregion
    }
}
