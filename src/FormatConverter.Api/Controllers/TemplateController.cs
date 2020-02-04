using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;
        private readonly IDbRepository _dbRepository;
        private readonly IDocPdfConverter _docPdfConverter;
        
        public TemplateController(ITemplateService templateService, IDbRepository dbRepository, IDocPdfConverter docPdfConverter)
        {
            _templateService = templateService;
            _dbRepository = dbRepository;
            _docPdfConverter = docPdfConverter;
        }

        [HttpGet]
        public ActionResult<List<TemplateFile>> Get()
        {
            var templateFiles = _dbRepository.Get<TemplateFile>().Include(x => x.File).ToList();
            return Ok(templateFiles);
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(TemplateFileModel templateFileModel)
        {
            if (templateFileModel == null)  
            {
                return BadRequest("Template was empty");
            }
            
            var templateFile = await _templateService.Create(templateFileModel);
            
            return Ok(templateFile.Id);
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
        
        [HttpPost("validate")]
        public async Task<ActionResult<IEnumerable<string>>> ValidateTemplate(TemplateValidateModel model)
        {
            var result = await _docPdfConverter.Validate(model);
            return Ok(result);
        }
    }
}