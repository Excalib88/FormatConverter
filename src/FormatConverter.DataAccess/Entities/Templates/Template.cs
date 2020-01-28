using System;

namespace FormatConverter.DataAccess.Entities.Templates
{
    public class Template: BaseEntity
    {
        public Uri Link { get; set; }
        public byte[] File { get; set; }
    }
}