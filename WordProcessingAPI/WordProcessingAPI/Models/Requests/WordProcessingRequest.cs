using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordProcessingAPI.Services;

namespace WordProcessingAPI.Models.Requests
{
    public class WordProcessingRequest
    {
        public string SentenceStrings { get; set; }

        public string ConvertMethod { get; set; }
    }
}
