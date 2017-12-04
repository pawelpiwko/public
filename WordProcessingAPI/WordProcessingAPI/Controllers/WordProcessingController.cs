using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordProcessingAPI.Services;
using WordProcessingAPI.Models.Requests;

namespace WordProcessingAPI.Controllers
{
    [Route("api/wordprocessing")]
    public class WordProcessingController : Controller
    {
        private readonly IWordProcessingService _wordProcessingService;

        public WordProcessingController(IWordProcessingService wordProcessingService)
        {
            _wordProcessingService = wordProcessingService;
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> PostConvert([FromBody]WordProcessingRequest request)
        {
            var processResult = await _wordProcessingService.ProcessSentenceAsync(request.SentenceStrings);
            if (processResult != null)
            {
                WordConvertingMethod method = ParseStringToEnum(request.ConvertMethod);
                string convertResult = await _wordProcessingService.ConvertSentencesAsync(processResult, method);
                if (!string.IsNullOrEmpty(convertResult))
                {
                    return Ok(convertResult);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("xml")]
        public async Task<IActionResult> PostConvertXML([FromBody]string value)
        {
            var processResult = await _wordProcessingService.ProcessSentenceAsync(value);
            if (processResult != null)
            {
                string convertResult = await _wordProcessingService.ConvertSentencesAsync(processResult, WordConvertingMethod.XML);
                if (!string.IsNullOrEmpty(convertResult))
                {
                    return Ok(convertResult);
                }
            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("csv")]
        public async Task<IActionResult> PostConvertCSV([FromBody]string value)
        {
            var processResult = await _wordProcessingService.ProcessSentenceAsync(value);
            if (processResult != null)
            {
                string convertResult = await _wordProcessingService.ConvertSentencesAsync(processResult, WordConvertingMethod.CSV);
                if (!string.IsNullOrEmpty(convertResult))
                {
                    return Ok(convertResult);
                }
            }
            return BadRequest();
        }



        private WordConvertingMethod ParseStringToEnum(string method)
        {
            if (!string.IsNullOrWhiteSpace(method))
            {
                return (WordConvertingMethod)System.Enum.Parse(typeof(WordConvertingMethod), method);
            }
            else
            {
                throw new Exception("Convert method is not valid!");
            }
        }

    }
}
