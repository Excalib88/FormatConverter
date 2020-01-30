using System;
using System.Linq;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities;
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

        public async Task<TemplateFile> Create(TemplateFileModel templateFileModel)
        {
            var existTemplateFile = await _dbRepository
                .Get<TemplateFile>()
                .FirstOrDefaultAsync(x => x.Link == templateFileModel.Link);

            if (existTemplateFile != null)
            {
                //log
                return null;
            }

            var templateContent = await FileHelper.Download(templateFileModel.Link);
            var fullName = templateFileModel.FullName;
                
            var templateFile = new TemplateFile
            {
                Fullname = fullName,
                Link = templateFileModel.Link,
                File = new File
                {
                    FullName = fullName,
                    Content = templateContent
                }
            };
                
            await _dbRepository.Add(templateFile);
            await _dbRepository.SaveChanges();
            
            return templateFile;
        }
    }
}