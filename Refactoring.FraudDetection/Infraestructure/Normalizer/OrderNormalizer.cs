using Microsoft.Extensions.Configuration;
using Refactoring.FraudDetection.Core.Normalizer;
using Refactoring.FraudDetection.Models;
using System;
using System.Configuration;

namespace Refactoring.FraudDetection.Infraestructure.Transformers
{
    public class OrderNormalizer : INormalizer<OrderModel>
    {
        private readonly IConfiguration _config;

        public OrderNormalizer(IConfiguration config)
        {
            _config = config;
        }

        public OrderModel Normalize(OrderModel order)
        {
            //Normalize email
            var aux = order.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            order.Email = string.Join("@", new string[] { aux[0], aux[1] });

            //Normalize street
            order.Street = _config.GetSection(order.Street).Value ?? order.Street;

            //Normalize state
            order.State = _config.GetSection(order.State).Value ?? order.State;

            return order;
        }
    }
}
