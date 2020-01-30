using System.Threading.Tasks;
using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities;

namespace FormatConverter.Core.Services
{
    public interface IConverter
    {
        Task<File> Convert(PrintFormModel file);
    }
}