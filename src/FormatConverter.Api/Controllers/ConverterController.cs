using System.Collections.Generic;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConverterController : Controller
    {
        private readonly IDocPdfConverter _docPdfConverter;
        
        public ConverterController(IDocPdfConverter docPdfConverter)
        {
            _docPdfConverter = docPdfConverter;
        }
        
        [HttpPost]
        public async Task<IActionResult> DocToPdf(PrintFormModel printFormModel)
        {
            if (printFormModel == null)
            {
                return BadRequest("PrintFormModel was null");
            }
            
            var result = await _docPdfConverter.Convert(printFormModel);
            
            return File(result.Content, "application/octet-stream", $"{result.FullName}");
        }
    }
}