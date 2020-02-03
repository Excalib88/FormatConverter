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
        private readonly IConverter _converter;
        
        public ConverterController(IConverter converter)
        {
            _converter = converter;
        }
        
        [HttpPost]
        public async Task<IActionResult> DocToPdf(PrintFormModel printFormModel)
        {
            if (printFormModel == null)
            {
                return BadRequest("PrintFormModel was null");
            }
            
            var result = await _converter.Convert(printFormModel);
            
            if (result == null)
            {
                return BadRequest("Result was null");
            }
            
            return Ok(result);
        }
    }
}