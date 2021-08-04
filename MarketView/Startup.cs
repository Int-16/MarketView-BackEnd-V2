using log4net;
using log4net.Config;
using MarketView.Commons;
using MarketView.Data;
using MarketView.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceStack.Messaging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketView
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration =  configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddControllers().AddNewtonsoftJson();

            //DependencyInjections
            services.AddCors();
            services.AddScoped<IDataEngine, DataEngine>();
            services.AddScoped<IMutualFundLibrary, MutualFundLibrary>();
            services.AddScoped<IMongoHandler, MongoHandler>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<MongoConfigurations>();
            services.AddScoped<IDataHandler, MongoHandler>();
            services.AddScoped<ICalculationEngine, CalculationEngine>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }

    }
}
