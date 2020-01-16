using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Models
{
    public class FraudResultModel
    {
        public int OrderId { get; set; }

        public bool IsFraudulent { get; set; }
    }
}
