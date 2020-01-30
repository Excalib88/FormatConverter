using FormatConverter.Abstractions;

namespace FormatConverter.DataAccess.Entities
{
    public class File: BaseEntity, IFile
    {
        public FileType Type { get; set; }
        public string FullName { get; set; }
        public byte[] Content { get; set; }
    }
}