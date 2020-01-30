using System.IO;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.DataAccess.Repositories;
using FormatConverter.Utils;
using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;
using File = FormatConverter.DataAccess.Entities.File;

namespace FormatConverter.Core.Services
{
    public class DocPdfConverter: IConverter
    {
        private readonly IDbRepository _dbRepository;
        
        public DocPdfConverter(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }
        
        public async Task<File> Convert(PrintFormModel printFormModel)
        {
            var template = await _dbRepository
                .Get<TemplateFile>()
                .Include(x => x.File)
                .FirstOrDefaultAsync(x => x.Link == printFormModel.Template.Link);

            if (template == null)
            {
                //log
                return null;
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

                    var file =  new File
                    {
                        FullName = printFormModel.Template.FullName,
                        Content = outputStream.ToArray()
                    };

                    var targetFileName = FileHelper.ChangeFileExtension(file.FullName);
                    await System.IO.File.WriteAllBytesAsync(targetFileName, file.Content);
                    
                    return file;
                }
            }
        }
    }
}