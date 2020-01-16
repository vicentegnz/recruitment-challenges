using Refactoring.FraudDetection.Core;
using Refactoring.FraudDetection.Models;
using System.Collections.Generic;

namespace Refactoring.FraudDetection.Infraestructure
{
    public class CheckFraudulentOrderService : ICheckFraudulentOrderService
    {
        #region Public Methods

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
                    if (AreFraudulentsOrders(current, possibleFraudulentOrder))
                    {
                        fraudResults.Add(new FraudResultModel { IsFraudulent = true, OrderId = possibleFraudulentOrder.OrderId });
                    }
                }
            }

            return fraudResults;
        }

        #endregion  

        #region Private Methods

        private bool AreFraudulentsOrders(OrderModel order, OrderModel possibleFraudulentOrder)
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

        #endregion
    }
}
