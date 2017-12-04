using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Models;

namespace WordProcessingAPI.Services
{
    public interface IWordProcessingService
    {
       Task< SentenceProcessResult> ProcessSentenceAsync(string sentence);

        Task<string> ConvertSentencesAsync(SentenceProcessResult sentences, WordConvertingMethod method);
    }
}
