using System.IO;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;

namespace FormatConverter.Core.Services
{
    public class DocPdfConverter: IConverter
    {
        public string Convert(IPrintFormModel printFormModel)
        {
            var stream = printFormModel.Template.OpenReadStream();
            var wordDocument = new WordDocument(stream, FormatType.Docx);
            wordDocument.ReplaceSingleLine("${name}","damir", false, false);
            var render = new DocIORenderer();
            render.Settings.ChartRenderingOptions.ImageFormat =  ExportImageFormat.Jpeg;
            var pdfDocument = render.ConvertToPDF(wordDocument);
            var outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);
            using (var fs = new FileStream("Template12.pdf", FileMode.Create, FileAccess.Write))
            {
                outputStream.WriteTo(fs);
            }

            return outputStream.ToString();
        }
    }
}