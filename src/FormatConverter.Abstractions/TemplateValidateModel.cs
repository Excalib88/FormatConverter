using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FormatConverter.Abstractions
{
    public class TemplateValidateModel
    {
        public byte[] File { get; set; }
        public List<string> Schema { get; set; }
    }

    public class TemplateTagsModel
    {
        public List<string> ValidTags { get; set; }
        public List<string> InvalidTags { get; set; }
    }
}