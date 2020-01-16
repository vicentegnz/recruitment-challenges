// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactoring.FraudDetection.Core;
using Refactoring.FraudDetection.Core.Normalizer;
using Refactoring.FraudDetection.Core.Reader;
using Refactoring.FraudDetection.Infraestructure;
using Refactoring.FraudDetection.Infraestructure.Transformers;
using Refactoring.FraudDetection.Models;
using Moq;

namespace Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {
        private static ICheckFraudulentOrderService _checkFraudulentOrderService;
        private static INormalizer<OrderModel> _normalizer;
        private static IReader<OrderModel>  _orderReader;

        [TestInitialize]
        public void Initialize()
        {
            _checkFraudulentOrderService = new CheckFraudulentOrderService();

            IConfigurationBuilder configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Environment.CurrentDirectory, "Conf"))
                .AddJsonFile("Normalization.json");

            _normalizer = new OrderNormalizer(configuration.Build());
            _orderReader = new OrderReader(_normalizer);
        }

        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {

            List<OrderModel> orders = _orderReader.GetDataFromFile(Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt")).ToList();

            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            List<OrderModel> orders = _orderReader.GetDataFromFile(Path.Combine(Environment.CurrentDirectory, "Files", "TwoLines_FraudulentSecond.txt")).ToList();

            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            List<OrderModel> orders = _orderReader.GetDataFromFile(Path.Combine(Environment.CurrentDirectory, "Files", "ThreeLines_FraudulentSecond.txt")).ToList();

            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            List<OrderModel> orders = _orderReader.GetDataFromFile(Path.Combine(Environment.CurrentDirectory, "Files", "FourLines_MoreThanOneFraudulent.txt")).ToList();

            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
        }

        private static List<FraudResultModel> ExecuteTest(List<OrderModel> orders)
        {
            var fraudRadar = new FraudRadar(_checkFraudulentOrderService);

            return fraudRadar.Check(orders).ToList();
        }
    }
}