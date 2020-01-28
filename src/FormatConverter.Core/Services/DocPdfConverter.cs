using System.IO;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;
using File = FormatConverter.Abstractions.File;

namespace FormatConverter.Core.Services
{
    public class DocPdfConverter: IConverter
    {
        private readonly IDbRepository _dbRepository;

        public DocPdfConverter(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public async Task<File> Convert(IPrintFormModel printFormModel)
        {
            var template = await _dbRepository
                .Get<TemplateFile>()
                .FirstOrDefaultAsync(x => x.Link == printFormModel.Template.Link);

            if (template.File == null)
            {
                //download
                template.File = new File();
                await _dbRepository.Update(template);
            }

            await using (var stream = new MemoryStream(template.File.Content))
            {
                var wordDocument = new WordDocument(stream, FormatType.Docx);

                foreach (var (key, value) in printFormModel.Schema)
                {
                    wordDocument.ReplaceSingleLine(key, value, false, false);
                }
                
                var render = new DocIORenderer();
                render.Settings.ChartRenderingOptions.ImageFormat =  ExportImageFormat.Jpeg;
                
                await using (var outputStream = new MemoryStream())
                {
                    var pdfDocument = render.ConvertToPDF(wordDocument);
                    pdfDocument.Save(outputStream);

                    return new File
                    {
                        FullName = printFormModel.Template.FullName,
                        Content = outputStream.ToArray()
                    };
                }
                
            }
        }
    }
}