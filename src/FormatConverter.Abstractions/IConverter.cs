using System.IO;
using System.Threading.Tasks;

namespace FormatConverter.Abstractions
{
    public interface IConverter
    {
        string Convert(IPrintFormModel file);
    }
}