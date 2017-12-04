using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordProcessingAPI.Models
{
    public class Sentence
    {
        public string[] Words { get; set; }

        public Sentence()
        {
        }

        public Sentence(string sentence)
        {
            ConvertToWords(sentence);
        }

        public void ConvertToWords(string sentence)
        {
            Words = sentence.Split(new char[] { ' ', ',', ';', ':', '.', '/', '\\', '(', ')', '[', ']', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
