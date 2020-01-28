using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    public class ConverterController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> DocToPdf(IFormFile uploadTemplate)
        {
            
            return Ok();
        }
    }
}