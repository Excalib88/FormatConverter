using System.Collections.Generic;

namespace FormatConverter.Abstractions
{
    public class PrintFormModel:IPrintFormModel
    {
        public TemplateFileModel Template { get; set; }
        public IDictionary<string, string> Schema { get; set; }
        public bool IsSaveTemplate { get; set; }
    }
}