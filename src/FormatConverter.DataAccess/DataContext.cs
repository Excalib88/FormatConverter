using FormatConverter.Abstractions;
using FormatConverter.DataAccess.Entities;
using FormatConverter.DataAccess.Entities.Templates;
using Microsoft.EntityFrameworkCore;

namespace FormatConverter.DataAccess
{
    public class DataContext: DbContext
    {
        public DbSet<TemplateFile> Templates { get; set; }
        public DbSet<ConvertHistory> ConvertHistories { get; set; }
        public DbSet<File> Files { get; set; } 
        
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }
    }
}