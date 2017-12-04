using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WordProcessingAPI.Controllers;
using WordProcessingAPI.Models;
using WordProcessingAPI.Services;
using Xunit;

namespace WordProcessingAPI.UnitTests.Controllers
{
    public class WordProcessingControllerTests
    {
        private IWordProcessingService _wordProcessingService;

        private SentenceProcessResult _sentenceParseResult = new SentenceProcessResult()
        {
            Sentences = new List<Sentence>() {
                    new Sentence("Mary had a little lamb."),
                    new Sentence("Peter called for the wolf, and Aesop came."),
                    new Sentence("Cinderella likes shoes.")
                }
        };


        public WordProcessingControllerTests()
        {
            _wordProcessingService = Substitute.For<IWordProcessingService>();
        }

        [Theory]
        [InlineData("XML", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        [InlineData("CSV", "Mary had a little lamb. Peter called for the wolf, and Aesop came. Cinderella likes shoes.")]
        public async Task WordProcrssingControllerBasicTest_Success(string convertMethodString, string stringToConvert)
        {           
            _wordProcessingService.ProcessSentenceAsync(Arg.Any<string>()).Returns(_sentenceParseResult);
            _wordProcessingService.ConvertSentencesAsync(Arg.Any<SentenceProcessResult>(), Arg.Any<WordConvertingMethod>()).Returns("<xml>result</xml>");
            WordProcessingController _sut = new WordProcessingController(_wordProcessingService);

            var response = await _sut.PostConvert(new Models.Requests.WordProcessingRequest() { ConvertMethod = convertMethodString, SentenceStrings = stringToConvert });
            
            var okResult = response as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
