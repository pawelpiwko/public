using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Models;
using System.Linq.Expressions;

namespace WordProcessingAPI.Extensions
{
    public static class SentenceExtensions
    {
        public static Sentence OrderAlphabetically(this Sentence sentence)
        {
            Array.Sort(sentence.Words, StringComparer.InvariantCulture);
            return sentence;
        }

        public static Sentence RemoveEmptyWords(this Sentence sentence)
        {
            sentence.Words = sentence.Words.Where(d => !string.IsNullOrWhiteSpace(d)).ToArray();
            
            return sentence;
        }
    }
}
