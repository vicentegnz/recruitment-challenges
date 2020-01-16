using Refactoring.FraudDetection.Core;
using Refactoring.FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Infraestructure
{
    public class CheckFraudulentOrderService : ICheckFraudulentOrderService
    {
        public List<FraudResultModel> GetAllFraudulentsOrders(List<OrderModel> orders)
        {

            var fraudResults = new List<FraudResultModel>();
            
            // CHECK FRAUD
            for (int i = 0; i < orders.Count; i++)
            {
                var current = orders[i];

                for (int j = i + 1; j < orders.Count; j++)
                {
                    OrderModel possibleFraudulentOrder = orders[j];
                    if (IsFraudulentOrder(current, possibleFraudulentOrder))
                    {
                        fraudResults.Add(new FraudResultModel { IsFraudulent = true, OrderId = possibleFraudulentOrder.OrderId });
                    }
                }
            }


            return fraudResults;

        }


        private bool IsFraudulentOrder(OrderModel order, OrderModel possibleFraudulentOrder)
        {

            if (order.CreditCard == possibleFraudulentOrder.CreditCard || order.DealId != possibleFraudulentOrder.DealId)
            {
                return false;
            }


            if (order.Email == possibleFraudulentOrder.Email ||
                (order.State == possibleFraudulentOrder.State
                && order.ZipCode == possibleFraudulentOrder.ZipCode
                && order.Street == possibleFraudulentOrder.Street
                && order.City == possibleFraudulentOrder.City))
            {
                return true;
            }

         
            return false;

        }
    }
}
