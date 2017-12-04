using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WordProcessingAPI.Models;
using WordProcessingAPI.Services;
using Xunit;

namespace WordProcessingAPI.UnitTests.Helpers
{
    public class CSVWordConverterTests
    {
        private readonly CSVWordConverter _sut;

        public CSVWordConverterTests()
        {
            _sut = new CSVWordConverter();
        }

        [Fact]
        public async Task ConvertToCSVFromSentences()
        {
            SentenceProcessResult sentences = new SentenceProcessResult();
            sentences.Sentences.Add(new Sentence("Mary had a little lamb."));
            sentences.Sentences.Add(new Sentence("Peter called for the wolf, and Aesop came. "));
            sentences.Sentences.Add(new Sentence("Cinderella likes shoes."));
            
            string csvResultString = await _sut.ConvertSentencesAsync(sentences);
            
            Assert.NotEmpty(csvResultString);
        }
    }
}
