using System.Collections.Generic;
using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities;

namespace FormatConverter.Core.Services
{
    public interface IDocPdfConverter
    {
        Task<File> Convert(PrintFormModel file);
        Task<TemplateTagsModel> Validate(TemplateValidateModel model);
    }
}