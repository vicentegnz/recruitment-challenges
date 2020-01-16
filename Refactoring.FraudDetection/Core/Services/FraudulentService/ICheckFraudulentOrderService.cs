using Refactoring.FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Core
{
    public interface ICheckFraudulentOrderService
    {
        List<FraudResultModel> GetAllFraudulentsOrders(List<OrderModel> orders);

    }
}
