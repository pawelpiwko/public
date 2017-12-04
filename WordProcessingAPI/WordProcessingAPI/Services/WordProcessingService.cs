using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Models;
using WordProcessingAPI.Extensions;
using WordProcessingAPI.Helpers;

namespace WordProcessingAPI.Services
{
    public class WordProcessingService : IWordProcessingService
    {
        private readonly IWordConverterProvider _converterProvider;

        private static char[] _sentenceSplitters = new char[] { '.', '!', '?' };

        public WordProcessingService(IWordConverterProvider converterProvider)
        {
            _converterProvider = converterProvider;
        }

        public async Task<SentenceProcessResult> ProcessSentenceAsync(string inputSentences)
        {
            SentenceProcessResult result = new SentenceProcessResult();

            List<string> sentenceStrings = inputSentences.Split(_sentenceSplitters, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var tempSentence in sentenceStrings)
            {
                if (!string.IsNullOrEmpty(tempSentence))
                {
                    result.Sentences.Add(new Sentence(tempSentence).RemoveEmptyWords().OrderAlphabetically());
                }
            }
            return result;
        }

        public async Task<string> ConvertSentencesAsync(SentenceProcessResult sentences, WordConvertingMethod method)
        {
            IWordConverter converter = _converterProvider.GetConverterByMethod(method);
            
            return await converter.ConvertSentencesAsync(sentences);
        }        
    }
}
