using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WordProcessingAPI.Models;
using WordProcessingAPI.Services;
using Xunit;

namespace WordProcessingAPI.UnitTests.Helpers
{
    public class XMLWordConverterTests
    {
        private readonly XMLWordConverter _sut;

        public XMLWordConverterTests()
        {
            _sut = new XMLWordConverter();
        }
        
        [Fact]
        public async Task ConvertToXMLFromSentences()
        {
            SentenceProcessResult sentences = new SentenceProcessResult();
            sentences.Sentences.Add(new Sentence("Mary had a little lamb."));
            sentences.Sentences.Add(new Sentence("Peter called for the wolf, and Aesop came. "));
            sentences.Sentences.Add(new Sentence("Cinderella likes shoes."));


            string xmlResultString = await _sut.ConvertSentencesAsync(sentences);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlResultString);
            var nodeResult = xmlDoc.SelectSingleNode("/text/sentence/word");

            Assert.NotNull(nodeResult);
            Assert.Equal("Mary", nodeResult.InnerText);
            Assert.NotEmpty(xmlResultString);
        }
    }
}
