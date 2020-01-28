using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FormatConverter.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile uploadTemplate)
        {
            var ms = await GetStream(uploadTemplate);
            var converter = new Converter();
            await converter.Convert(ms);
            
            return Ok();
        }
        
        
        private async Task<MemoryStream> GetStream(IFormFile formFile)
        {
            var memoryStream = new MemoryStream();
            
            await formFile.CopyToAsync(memoryStream);
            return memoryStream;
            
        }
    }
}