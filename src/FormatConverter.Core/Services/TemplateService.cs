using System;
using System.Linq;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using FormatConverter.Utils;
using Microsoft.EntityFrameworkCore;

namespace FormatConverter.Core.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IDbRepository _dbRepository;

        public TemplateService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public async Task<Guid> Create(IPrintFormModel printFormModel)
        {
            var link = printFormModel.Template.Link;
            
            var templateFile = await _dbRepository
                .Get<TemplateFile>()
                .FirstOrDefaultAsync(x => x.Link == link);

            if (templateFile == null)
            {
                var templateContent = FileHelper.Download(printFormModel.Template.Link);
                
                templateFile = new TemplateFile
                {
                    Fullname = printFormModel.Template.FullName,
                    Link = link,
                    File = new File
                    {
                        Content = templateContent
                    }
                };

                if (printFormModel.IsSaveTemplate)
                {
                    await _dbRepository.Add(templateFile);
                }
            }
            
            return Guid.NewGuid();
        }
    }
}