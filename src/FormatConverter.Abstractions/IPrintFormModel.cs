using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace FormatConverter.Abstractions
{
    public interface IPrintFormModel
    {
        TemplateFileModel Template { get; set; } 
        IDictionary<string, string> Schema { get; set; }
        bool IsSaveTemplate { get; set; }
    }
}