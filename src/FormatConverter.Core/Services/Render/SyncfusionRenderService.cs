using System.IO;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;

namespace FormatConverter.Core.Services.Render
{
    public class SyncfusionRenderService: IRenderService
    {
        public async Task<byte[]> Render(PrintFormModel printFormModel, TemplateFile template)
        {
            await using (var stream = new MemoryStream(template.File.Content))
            {
                var wordDocument = new WordDocument(stream, FormatType.Docx);

                foreach (var (key, value) in printFormModel.Schema)
                {
                    wordDocument.ReplaceSingleLine(key, value, false, false);
                }

                var render = new DocIORenderer();
                render.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;

                await using (var outputStream = new MemoryStream())
                {
                    var pdfDocument = render.ConvertToPDF(wordDocument);
                    pdfDocument.Save(outputStream);

                    return outputStream.ToArray();
                }
            }
        }
    }
}