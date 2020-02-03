using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;
using FormatConverter.Utils;

namespace FormatConverter.Core.Services.Render
{
    public class OpenXmlRenderService: IRenderService
    {
        public async Task<byte[]> Render(PrintFormModel printFormModel, TemplateFile templateFile)
        {
            var tempPath = $"temp/{templateFile.Fullname}";
            await using (var fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                await fs.WriteAsync(templateFile.File.Content);
            }
            
            using (var wordDocument = WordprocessingDocument.Open(tempPath, true))
            {
                string docText;
                using (var sr = new StreamReader(wordDocument.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                foreach (var (key, value) in printFormModel.Schema)
                {
                    var regexKey = new Regex(key);
                    docText = regexKey.Replace(docText, value);
                }
                
                using (var sw = new StreamWriter(wordDocument.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    await sw.WriteAsync(docText);
                }
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