using System.Collections.Generic;
using WordProcessingAPI.Services;

namespace WordProcessingAPI.Helpers
{
    public interface IWordConverterProvider
    {
        IWordConverter GetConverterByMethod(WordConvertingMethod method);
    }
}