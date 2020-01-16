using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.CountingBits.Core
{
    public interface IPositiveBitCounterService
    {
        List<int> GetAllIndexesOfPositiveBitMatches(string input, string match);
    }
}
