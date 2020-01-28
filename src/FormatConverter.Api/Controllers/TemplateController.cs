using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services;
using FormatConverter.DataAccess.Entities.Templates;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;
        
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }
        
        [HttpGet]
        public ActionResult<TemplateFile> Get(Guid id)
        {
            return Ok(new TemplateFile());
        }

        [HttpGet]
        public ActionResult<List<TemplateFile>> GetAll()
        {
            return Ok(new List<TemplateFile>());
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(IPrintFormModel printFormModel)
        { 
            if (printFormModel.Template == null || printFormModel.Template.Content?.Length <= 0)
            {
                return BadRequest("Template was empty");
            }

            var templateId = await _templateService.Create(printFormModel);

            return Ok(templateId);
        }

        [HttpPut]
        public ActionResult<Guid> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Guid was empty");
            }
            
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}