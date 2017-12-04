using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordProcessingAPI.Models
{
    public class SentenceProcessResult
    {
        public List<Sentence> Sentences { get; set; }

        public SentenceProcessResult()
        {
            Sentences = new List<Sentence>();
        }
    }
}
