using Refactoring.FraudDetection.Core.Normalizer;
using Refactoring.FraudDetection.Core.Reader;
using Refactoring.FraudDetection.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Refactoring.FraudDetection.Infraestructure
{
    public class OrderReader : IReader<OrderModel>
    {
        #region Properties

        private readonly INormalizer<OrderModel> _orderNormalizer;

        #endregion

        #region Ctor

        public OrderReader(INormalizer<OrderModel> normalizer)
        {
            _orderNormalizer = normalizer;
        }

        #endregion

        #region Public Methods

        public virtual IEnumerable<OrderModel> GetDataFromFile(string filePath)
        {
            var orders = new List<OrderModel>();
            using (StreamReader sr = File.OpenText(filePath))
            {
                var line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    var items = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    var order = new OrderModel
                    {
                        OrderId = int.Parse(items[0]),
                        DealId = int.Parse(items[1]),
                        Email = items[2].ToLower(),
                        Street = items[3].ToLower(),
                        City = items[4].ToLower(),
                        State = items[5].ToLower(),
                        ZipCode = items[6],
                        CreditCard = items[7]
                    };


                    orders.Add(_orderNormalizer.Normalize(order));
                }
            }

            return orders;
        }

        #endregion

    }
}
