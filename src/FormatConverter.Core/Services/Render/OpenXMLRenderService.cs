using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.Utils;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace FormatConverter.Core.Services.Render
{
    public class OpenXmlRenderService: IRenderService
    {
        public async Task<byte[]> Render(PrintFormModel printFormModel, TemplateFile templateFile)
        {
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }
            
            var tempPath = $"temp/{templateFile.Fullname}";
            await using (var fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await fs.WriteAsync(templateFile.File.Content);
            }
            
            using (var wordDocument = WordprocessingDocument.Open(tempPath, true))
            {
                var document = wordDocument.MainDocumentPart.Document;

                foreach (OpenXmlElement childElement in document.ChildElements)
                {
                    if (childElement.InnerText != "")
                    {
                        
                    }
                }

                foreach (var (key, value) in printFormModel.Schema)
                {
                    foreach (var text in document.Body.Descendants<Text>())
                    {
                        if (text.Text.Contains(key))
                        {
                            text.Text = text.Text.Replace(key, value);
                        }
                    }
                }

                wordDocument.SaveAs(@"C:\projects\FormatConverter\src\FormatConverter.Api\dasd.docx");
                wordDocument.Close();
            }

            byte[] result;
            
            await using (var outputMs = new MemoryStream())
            {
                await using (var outputFs =
                    new FileStream(tempPath, FileMode.Open, FileAccess.Read))
                {
                    await outputFs.CopyToAsync(outputMs);
                    result = outputMs.ToArray();
                }
            }
            
            FileHelper.DeleteFilesFromDirectory("temp");
            return result;
        }
    }
}