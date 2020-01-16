// <copyright file="PositiveBitCounterTest.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Algorithms.CountingBits.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Algorithms.CountingBits.Core;
    using Algorithms.CountingBits.Infraestructure;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositiveBitCounterTest
    {

        private static IPositiveBitCounterService _positiveBitCounterService;

        [TestInitialize]
        public void Initialize()
        {
            _positiveBitCounterService = new PositiveBitCounterService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Count_NegativeValue_ArgumentExceptionExpected()
        {
            ExecuteTest(-2);
        }

        [TestMethod]
        public void Count_Zero_NoOccurrences()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 0 },
                actual: ExecuteTest(0),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_OneOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 1, 0 },
                actual: ExecuteTest(1),
                message: "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_MultipleOcurrence()
        {
            CollectionAssert.AreEqual(
                expected: new List<int> { 3, 0, 5, 7 },
                actual: ExecuteTest(161),
                message: "The result is not the expected");
        }

         private static List<int> ExecuteTest(int input)
        {
            var _bitCounter = new PositiveBitCounter(_positiveBitCounterService);

            return _bitCounter.Count(input).ToList();
        }
    }
}