using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services.Render;
using FormatConverter.DataAccess;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using FormatConverter.Utils;
using Microsoft.EntityFrameworkCore;
using File = FormatConverter.DataAccess.Entities.File;

namespace FormatConverter.Core.Services
{
    public class DocPdfConverter: IConverter
    {
        private readonly IDbRepository _dbRepository;
        private readonly IRenderService _renderService;
        
        public DocPdfConverter(IDbRepository dbRepository, IRenderService renderService)
        {
            _dbRepository = dbRepository;
            _renderService = renderService;
        }
        
        public async Task<File> Convert(PrintFormModel printFormModel)
        {
            var template = await _dbRepository
                .Get<TemplateFile>()
                .Include(x => x.File)
                .FirstOrDefaultAsync(x => x.Link == printFormModel.Template.Link);

            var renderedFile = await _renderService.Render(printFormModel, template);
            var targetFileName = FileHelper.ChangeFileExtension(printFormModel.Template.FullName);
            var file =  new File
            {
                Type = FileType.Converted,
                FullName = targetFileName,
                Content = renderedFile
            };

            await _dbRepository.Add(file);
            await _dbRepository.SaveChanges();
            
            // todo: удалить при релизе
            await System.IO.File.WriteAllBytesAsync(targetFileName, renderedFile);
                
            return file;
        }
    }
}