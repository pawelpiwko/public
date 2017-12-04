using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WordProcessingAPI.Services;
using WordProcessingAPI.Helpers;
using WordProcessingAPI.Filters;

namespace WordProcessingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IWordConverter, XMLWordConverter>();
            services.AddScoped<IWordConverter, CSVWordConverter>();
            services.AddScoped<IWordConverterProvider, WordConverterProvider>();
            services.AddScoped<IWordProcessingService, WordProcessingService>();

            var builder = services.AddMvc(d => d.Filters.Add(typeof(GlobalExceptionFilter)));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
