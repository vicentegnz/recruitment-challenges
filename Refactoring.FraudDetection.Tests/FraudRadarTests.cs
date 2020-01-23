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
        private static Mock<OrderReader>  _orderReader;

        private readonly string fileText1 = Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt");
        private readonly string fileText2 = Path.Combine(Environment.CurrentDirectory, "Files", "TwoLines_FraudulentSecond.txt");
        private readonly string fileText3 = Path.Combine(Environment.CurrentDirectory, "Files", "ThreeLines_FraudulentSecond.txt");
        private readonly string fileText4 = Path.Combine(Environment.CurrentDirectory, "Files", "FourLines_MoreThanOneFraudulent.txt");

        [TestInitialize]
        public void Initialize()
        {
            _checkFraudulentOrderService = new CheckFraudulentOrderService();

            IConfigurationBuilder configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Environment.CurrentDirectory, "Conf"))
                .AddJsonFile("Normalization.json");

            _normalizer = new OrderNormalizer(configuration.Build());
            _orderReader = new Mock<OrderReader>(_normalizer);

            _orderReader.Setup(x => x.GetDataFromFile(It.Is<string>(p => p == fileText1))).Returns(GetMockOrders_NoFraudExpected());
            _orderReader.Setup(x => x.GetDataFromFile(It.Is<string>(p => p == fileText2))).Returns(GetMockOrders_SecondOrderFraudulent());
            _orderReader.Setup(x => x.GetDataFromFile(It.Is<string>(p => p == fileText3))).Returns(GetMockOrders_SecondLineFraudulenForThreeOrders());
            _orderReader.Setup(x => x.GetDataFromFile(It.Is<string>(p => p == fileText4))).Returns(GetMockOrders_MoreThanOneFraudulent());

        }

        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {

            List<OrderModel> orders = _orderReader.Object.GetDataFromFile(fileText1).ToList();
                
                
            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            List<OrderModel> orders = _orderReader.Object.GetDataFromFile(fileText2).ToList();

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
            List<OrderModel> orders = _orderReader.Object.GetDataFromFile(fileText3).ToList();

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
            List<OrderModel> orders = _orderReader.Object.GetDataFromFile(fileText4).ToList();

            var result = ExecuteTest(orders);

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
        }


        #region Private Methods

        private static List<FraudResultModel> ExecuteTest(List<OrderModel> orders)
        {
            var fraudRadar = new FraudRadar(_checkFraudulentOrderService);

            return fraudRadar.Check(orders).ToList();
        }


        private List<OrderModel> GetMockOrders_MoreThanOneFraudulent()
        {
            return new List<OrderModel> { 
                new OrderModel { OrderId = 1, DealId = 1, Email = "bugs@bunny.com", State = "" , ZipCode= "123", City = "new york", CreditCard = "12345689010" },
                new OrderModel { OrderId = 2, DealId = 1, Email = "bugs@bunny.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689011" },
                new OrderModel { OrderId = 3, DealId = 2, Email = "roger@rabbit.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689012" },
                new OrderModel { OrderId = 4, DealId = 2, Email = "roger@rabbit.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689014" },
            };
        }

        private List<OrderModel> GetMockOrders_SecondOrderFraudulent()
        {
            return new List<OrderModel> {
                new OrderModel { OrderId = 1, DealId = 1, Email = "bugs@bunny.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689010" },
                new OrderModel { OrderId = 2, DealId = 1, Email = "bugs@bunny.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689011" } };

        }

        private List<OrderModel> GetMockOrders_NoFraudExpected()
        {
            return new List<OrderModel> { 
                new OrderModel { OrderId = 1, DealId = 1, Email = "bugs@bunny.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689010" } 
            };

        }
        private List<OrderModel> GetMockOrders_SecondLineFraudulenForThreeOrders()
        {
            return new List<OrderModel> { 
                new OrderModel { OrderId = 1, DealId = 1, Email = "bugs@bunny.com", State = "" , ZipCode= "123", City = "new york", CreditCard = "12345689010" },
                new OrderModel { OrderId = 2, DealId = 1, Email = "bugs@bunny.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689011" },
                new OrderModel { OrderId = 3, DealId = 2, Email = "roger@rabbit.com", State = "", ZipCode = "123", City = "new york", CreditCard = "12345689012" }
            };

        }

        #endregion

    }
}