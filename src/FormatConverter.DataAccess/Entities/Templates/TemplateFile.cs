using FormatConverter.Abstractions;

namespace FormatConverter.DataAccess.Entities.Templates
{
    public class TemplateFile: BaseEntity
    {
        public string Fullname { get; set; }

        public string Link { get; set; }

        public File File { get; set; }
    }
}