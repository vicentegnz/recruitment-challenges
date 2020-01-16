// <copyright file="PositiveBitCounter.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits
{
    using Algorithms.CountingBits.Core;
    using Algorithms.CountingBits.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PositiveBitCounter
    {
        #region Consts

        private const string CHAR_MATCH = "1";

        #endregion

        #region Properties

        private readonly IPositiveBitCounterService _positiveCounterService;

        #endregion

        #region Ctor

        public PositiveBitCounter(IPositiveBitCounterService positiveBitCounterService)
        {
            _positiveCounterService = positiveBitCounterService;
        }

        #endregion

        #region Public Methods

        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Input value must be a positive number.");
            }

            string binary = input.ToBinary();

            List<int> indexMatches = _positiveCounterService.GetAllIndexesOfPositiveBitMatches(binary.Reverse(), CHAR_MATCH);

            return indexMatches.Prepend(indexMatches.Count);
        }

        #endregion

    }
}
