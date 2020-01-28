using System;
using System.Threading.Tasks;
using FormatConverter.Abstractions;

namespace FormatConverter.Core.Services
{
    public interface ITemplateService
    {
        Task<Guid> Create(IPrintFormModel printFormModel);
    }
}