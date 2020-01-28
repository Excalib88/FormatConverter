using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;
        private readonly IDbRepository _dbRepository;
        
        public TemplateController(ITemplateService templateService, IDbRepository dbRepository)
        {
            _templateService = templateService;
            _dbRepository = dbRepository;
        }
        
        // возможно рест методы и не нужны вовсе)
        [HttpGet]
        public ActionResult<TemplateFile> Get(Guid id)
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<TemplateFile>> GetAll()
        {
            var templateFiles = _dbRepository.GetAll<TemplateFile>().ToList();
            
            return Ok(templateFiles);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(IPrintFormModel printFormModel)
        { 
            if (printFormModel.Template == null)
            {
                return BadRequest("Template was empty");
            }
            
            var templateFile = await _templateService.Create(printFormModel);

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
    }
}