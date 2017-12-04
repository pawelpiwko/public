using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Services;

namespace WordProcessingAPI.Helpers
{
    public class WordConverterProvider : IWordConverterProvider
    {
        private readonly IEnumerable<IWordConverter> _converters;

        public WordConverterProvider(IEnumerable<IWordConverter> list)
        {
            _converters = list;
        }

        public IWordConverter GetConverterByMethod(WordConvertingMethod method)
        {
            IWordConverter converter = null;
            if (_converters.Any(d => d.MethodName == method))
            {
                converter = _converters.First(d => d.MethodName == method);
            }
            else
            {
                throw new Exception("Cannot find format converter!");
            }
            return converter;
        }
    }
}
