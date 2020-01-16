using System;
using System.Collections.Generic;
using System.Text;

namespace Refactoring.FraudDetection.Core.Reader
{
    public interface IReader<T>
    {
        IEnumerable<T> GetDataFromFile(string filePath);
    }
}
