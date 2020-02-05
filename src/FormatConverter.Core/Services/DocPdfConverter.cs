using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.Core.Services.Render;
using FormatConverter.DataAccess;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using FormatConverter.Utils;
using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using File = FormatConverter.DataAccess.Entities.File;

namespace FormatConverter.Core.Services
{
    public class DocPdfConverter: IDocPdfConverter
    {
        private readonly IDbRepository _dbRepository;
        private readonly ITemplateService _templateService;
        private readonly IRenderService _renderService;
        
        public DocPdfConverter(IDbRepository dbRepository, ITemplateService templateService, IRenderService renderService)
        {
            _dbRepository = dbRepository;
            _templateService = templateService;
            _renderService = renderService;
        }
        
        public async Task<File> Convert(PrintFormModel printFormModel)
        {
            var template = await _dbRepository
                               .Get<TemplateFile>()
                               .Include(x => x.File)
                               .FirstOrDefaultAsync(x => x.Link == printFormModel.Template.Link) 
                           ?? await _templateService.Create(printFormModel.Template);

            var renderedFile = await _renderService.Render(printFormModel, template);
            var targetFileName = FileHelper.ChangeFileExtension(printFormModel.Template.FullName);
            var file =  new File
            {
                Type = FileType.Converted,
                FullName = targetFileName,
                Content = renderedFile
            };

            if (file.Content == null)
            {
                //log
                return null;
            }
            
            await _dbRepository.Add(file);
            await _dbRepository.SaveChanges();
            
            return file;
        }

        public async Task<TemplateTagsModel> Validate(TemplateValidateModel model)
        {
            var invalidTags = new List<string>();
            var validTags = new List<string>();
            await using (var stream = new MemoryStream(model.File))
            {
                var wordDocument = new WordDocument(stream, FormatType.Docx);
                var tags = wordDocument.FindAll(new Regex("\\$\\{.+?\\}"));
                foreach (var tag in tags)
                {
                    if (model.Schema.Contains(tag.SelectedText))
                        validTags.Add(tag.SelectedText);
                    else invalidTags.Add(tag.SelectedText);
                }
            }
            return new TemplateTagsModel
            {
                InvalidTags = invalidTags,
                ValidTags = validTags
            };
        }
    }
}