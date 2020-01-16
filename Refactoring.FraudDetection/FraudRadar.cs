// <copyright file="FraudRadar.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

namespace Refactoring.FraudDetection
{
    using Refactoring.FraudDetection.Core;
    using Refactoring.FraudDetection.Models;
    using System.Collections.Generic;

    public class FraudRadar
    {
        private readonly ICheckFraudulentOrderService _checkFraudulentOrderService;

        public FraudRadar(ICheckFraudulentOrderService checkFraudulentOrderService)
        {
            _checkFraudulentOrderService = checkFraudulentOrderService;
        }

        public IEnumerable<FraudResultModel> Check(List<OrderModel> orders)
        {
            return _checkFraudulentOrderService.GetAllFraudulentsOrders(orders);
        }
    }
}