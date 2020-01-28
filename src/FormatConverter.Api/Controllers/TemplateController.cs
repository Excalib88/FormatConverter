using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormatConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplateController : Controller
    {
        [HttpGet]
        public ActionResult<byte[]> Get(Guid id)
        {
           
            return Ok(new byte[1]);
        }

        [HttpGet]
        public ActionResult<List<byte[]>> GetAll()
        {
            return Ok(new List<byte[]>());
        }

        [HttpPost]
        public ActionResult<Guid> Create(IFormFile template)
        { 
            if (template == null || template.Length <= 0)
            {
                return BadRequest("Template was empty");
            }
            
            return Ok(Guid.NewGuid());
        }

        [HttpPut]
        public ActionResult<Guid> Update(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Guid was empty");
            }
            
            return Ok(Guid.NewGuid());
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            return Ok();
        }
    }
}