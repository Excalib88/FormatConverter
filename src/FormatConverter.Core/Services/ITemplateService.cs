using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities.Templates;

namespace FormatConverter.Core.Services
{
    public interface ITemplateService
    {
        Task<TemplateFile> Create(TemplateFileModel templateFileModel);
    }
}