using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Models;

namespace WordProcessingAPI.Services
{
    public interface IWordConverter
    {
        WordConvertingMethod MethodName { get; }
        
        Task<string> ConvertSentencesAsync(SentenceProcessResult sentences);
    }
}
