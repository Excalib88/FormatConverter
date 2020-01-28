using FormatConverter.DataAccess.Entities.Templates;

namespace FormatConverter.DataAccess.Entities
{
    public class ConvertHistory: BaseEntity
    {
        public ConvertType Type { get; set; }
        public Template Template { get; set; }
    }
}