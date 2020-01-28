using FormatConverter.DataAccess.Entities.Templates;

namespace FormatConverter.DataAccess.Entities
{
    public class ConvertHistory: BaseEntity
    {
        public ConvertType Type { get; set; }
        public TemplateFile Template { get; set; }
    }
}