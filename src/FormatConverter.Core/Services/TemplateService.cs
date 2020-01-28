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

        public async Task<TemplateFile> Create(IPrintFormModel printFormModel)
        {
            var templateContent = await FileHelper.Download(printFormModel.Template.Link);
            var fullName = printFormModel.Template?.FullName;
                
            var templateFile = new TemplateFile
            {
                Fullname = fullName,
                Link = printFormModel.Template?.Link,
                File = new File
                {
                    FullName = fullName,
                    Content = templateContent
                }
            };
                
            // добавить проверку на сохранение темплейта, но мне кажется можно убрать этот параметр
            await _dbRepository.Add(templateFile);

            return templateFile;
        }
    }
}