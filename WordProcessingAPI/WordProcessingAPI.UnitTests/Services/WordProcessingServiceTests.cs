using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordProcessingAPI.Helpers;
using WordProcessingAPI.Services;
using Xunit;

namespace WordProcessingAPI.UnitTests
{
    public class WordProcessingServiceTests
    {
        private readonly IWordProcessingService _sut;
        private readonly IWordConverterProvider _converterProvider;
        private readonly CSVWordConverter _csvConverter;
        private readonly XMLWordConverter _xmlConverter;

        private readonly List<IWordConverter> _converterList = new List<IWordConverter>();

        public WordProcessingServiceTests()
        {
            _csvConverter = new CSVWordConverter();
            _xmlConverter = new XMLWordConverter();
            _converterList.Add(_csvConverter);
            _converterList.Add(_xmlConverter);

            _converterProvider = new WordConverterProvider(_converterList);

            _sut = new WordProcessingService(_converterProvider);
        }

        [Fact]
        public async Task ProcessStringToSentences()
        {
            string input = "Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index. Attributes are not children of an Element node.";

            var result = await _sut.ProcessSentenceAsync(input);
            
            Assert.NotNull(result);
            Assert.NotEmpty(result.Sentences);
            Assert.Equal(2, result.Sentences.Count);
        }


        [Fact]
        public async Task ProcessStringToSentencesToXMLFormat()
        {
            string input = "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.";

            var result = await _sut.ProcessSentenceAsync(input);

            var formattedResult = await _sut.ConvertSentencesAsync(result, WordConvertingMethod.XML);
            
            Assert.NotEmpty(formattedResult);
        }

        [Fact]
        public async Task ProcessStringToSentencesToCSVFormat()
        {
            string input = "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.";

            var result = await _sut.ProcessSentenceAsync(input);

            var formattedResult = await _sut.ConvertSentencesAsync(result, WordConvertingMethod.CSV);
            
            Assert.NotEmpty(formattedResult);
        }
    }
}
