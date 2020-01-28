﻿using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    public class ConverterController : Controller
    {
        private readonly IConverter _converter;
        
        public ConverterController(IConverter converter)
        {
            _converter = converter;
        }
        
        [HttpPost]
        public async Task<IActionResult> DocToPdf(IPrintFormModel printFormModel)
        {
            if (printFormModel == null)
            {
                return BadRequest("PrintFormModel was null");
            }
            
            var result = await _converter.Convert(printFormModel);
            return Ok(result);
        }
    }
}