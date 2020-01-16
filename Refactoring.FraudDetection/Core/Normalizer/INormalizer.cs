

namespace Refactoring.FraudDetection.Core.Normalizer
{
    public interface INormalizer<T>
    {
        T Normalize(T value);
    }
}
