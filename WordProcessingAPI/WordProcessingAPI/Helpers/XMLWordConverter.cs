using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using WordProcessingAPI.Models;

namespace WordProcessingAPI.Services
{
    public class XMLWordConverter : IWordConverter
    {
        public WordConvertingMethod MethodName => WordConvertingMethod.XML;

        public async Task<string> ConvertSentencesAsync(SentenceProcessResult sentencesToProcess)
        {
            XmlDocument xmlDoc = new XmlDocument();
            var rootElement = xmlDoc.CreateElement("text");
            xmlDoc.AppendChild(rootElement);
            for (int i = 0; i < sentencesToProcess.Sentences.Count(); i++)
            {
                if (sentencesToProcess.Sentences[i].Words?.Count() > 0)
                {
                    XmlNode tempSentenceNode = CreateSentenceNode(xmlDoc, sentencesToProcess.Sentences[i]);
                    rootElement.AppendChild(tempSentenceNode);
                }
            }
            return xmlDoc.OuterXml.ToString();
        }

        private XmlNode CreateWordNode(XmlDocument xmlDoc, string word)
        {
            XmlNode wordNode = xmlDoc.CreateNode(XmlNodeType.Element, "word", null);
            wordNode.InnerText = word;
            return wordNode;
        }
        
        private XmlNode CreateSentenceNode(XmlDocument xmlDoc, Sentence sentenceTemp)
        {
            XmlNode tempSentenceNode = xmlDoc.CreateNode(XmlNodeType.Element, "sentence", null);

            for (int j = 0; j < sentenceTemp.Words.Count(); j++)
            {
                if (!string.IsNullOrEmpty(sentenceTemp.Words[j]))
                {
                    XmlNode wordTempNode = CreateWordNode(xmlDoc, sentenceTemp.Words[j]);
                    tempSentenceNode.AppendChild(wordTempNode);
                }
            }

            return tempSentenceNode;
        }
    }
}
