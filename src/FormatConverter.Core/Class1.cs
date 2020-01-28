using System;
using System.IO;
using System.Threading.Tasks;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;

namespace FormatConverter.Core
{
    public class Converter
    {
        public async Task Convert(Stream stream)
        {
            var wordDocument = new WordDocument(stream, FormatType.Docx);
            //var selection = wordDocument.Find(, false, false);
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
            stream.Dispose();
        }
    }
}