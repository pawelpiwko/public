using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessingAPI.Models;

namespace WordProcessingAPI.Services
{
    public class CSVWordConverter : IWordConverter
    {
        public WordConvertingMethod MethodName => WordConvertingMethod.CSV;

        private int maximumSentenceLenght = 0;

        public string CreateHeaderRow(int length)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 1; i <= length; i++)
            {
                sb.Append(", Word ");
                sb.Append(i);
            }
            return sb.ToString();
        }

        public async Task<string> ConvertSentencesAsync(SentenceProcessResult sentences)
        {
            StringBuilder sb = new StringBuilder("");
            int maxLenght = sentences.Sentences.Max(d => d.Words.Count());
            sb.AppendLine(CreateHeaderRow(maxLenght));

            for (int i = 0; i < sentences.Sentences.Count; i++)
            {
                if (sentences.Sentences[i].Words.Count() > 0)
                {
                    sb.Append("Sentence ");
                    sb.Append(i + 1);
                    sb.AppendLine(ConvertSentenceToString(sentences.Sentences[i]));
                }
            }

            return sb.ToString();
        }

        public string ConvertSentenceToString(Sentence sentence)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < sentence.Words.Count(); i++)
            {
                if (!string.IsNullOrEmpty(sentence.Words[i]))
                {
                    sb.Append(", ");
                    sb.Append(sentence.Words[i]);
                }
            }
            return sb.ToString();
        }
    }
}
