using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;

namespace FormatConverter.Core.Services.Render
{
    public interface IRenderService
    {
        Task<byte[]> Render(PrintFormModel printFormModel, TemplateFile templateFile, bool isNeedConvertToPdf = false);
    }
}