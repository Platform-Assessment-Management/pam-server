using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PAMApplication;
using PAMDomain;
using PAMDomain.Repositories;
using PAMRepository;
using PAMRepository.Configuration;
using PAMRepository.Impl;

namespace PAMCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PAMDatabaseSettings>(
                Configuration.GetSection(nameof(PAMDatabaseSettings)));

            services.AddSingleton<IPAMDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<PAMDatabaseSettings>>().Value);

            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var settings = (IPAMDatabaseSettings)sp.GetService(typeof(IPAMDatabaseSettings));

                var client = new MongoClient(settings.ConnectionString);
                return client.GetDatabase(settings.DatabaseName);
            });

            services.AddSingleton<ChapterApplication>();
            services.AddSingleton<PlatformApplication>();
            services.AddSingleton<ProjectApplication>();
            services.AddSingleton<MaturityModelApplication>();

            services.AddSingleton<IChapterRepository, ChapterRepository>();
            services.AddSingleton<IPlatformRepository, PlatformRepository>();
            services.AddSingleton<IProjectRepository, ProjectRepository>();
            services.AddSingleton<IMaturityModelRepository, MaturityModelRepository>();

            services.AddControllers();

            services.AddSwaggerGen();

            UtilDomain.SetService(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseGlobalExceptionHandler(loggerFactory);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigurationRepository.BSONMap();
        }
    }
}
