using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using FormatConverter.Core;
using FormatConverter.Core.Services;
using FormatConverter.Core.Services.Render;
using FormatConverter.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Syncfusion.Licensing;

namespace FormatConverter.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(
                    new SlugifyParameterTransformer()));
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Format Converter API", Version = "v1" });
            });
            
            services.AddSqLiteDatabase(_configuration);
            services.AddScoped<IConverter, DocPdfConverter>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IRenderService, OpenXmlRenderService>(); //SyncfusionRenderService
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            //serviceProvider.GetService<DataContext>().Database.Migrate();    
            app.UseRouting();
            app.UseAuthorization();
            
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Format Converter API v1");
                x.RoutePrefix = "swagger";
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
    
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object value) => value == null ? null : 
                Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}