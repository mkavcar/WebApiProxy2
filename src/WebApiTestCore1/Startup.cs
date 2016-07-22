using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebApiTestCore1.Interfaces;
using WebApiTestCore1.Models;
using WebApiTestCore1.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApiTestCore1
{
    public class Startup
    {
        /// <summary>
        /// Use Redis Cache in Staging
        /// </summary>
        /// <param name="services"></param>
        //public void ConfigureStagingServices(IServiceCollection services)
        //{

        //    services.AddDistributedRedisCache(options =>
        //    {
        //        options.Configuration = "localhost";
        //        options.InstanceName = "SampleInstance";
        //    });
        //}

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddDistributedMemoryCache();
            
            //DI
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IUserService, UserService>();

            services.AddDbContext<IDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors();
            services.AddMvc();
            services.AddSwaggerGen();
            services.AddDistributedMemoryCache();
            
            //DI
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IUserService, UserService>();

            services.AddDbContext<IDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDistributedCache cache)
        {
            loggerFactory.AddSerilog(new LoggerConfiguration()
                                    //.ReadFrom.Configuration(Configuration.GetSection("Serilog"))
                                    .MinimumLevel.Information()
                                    .WriteTo.Seq("http://localhost:5341")
                                    .CreateLogger());

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            app.UseCors(builder =>
            {
                builder.WithOrigins("*");
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();

            //Initialize in-mem cache for developemtn
            if (env.IsDevelopment())
            {
                //read countries from file
                //List<Country> items = JsonConvert.DeserializeObject<List<Country>>();

                cache.Set("CountryList", Encoding.ASCII.GetBytes(File.ReadAllText($"{env.ContentRootPath}/countrylist.json")));
            }
        }
    }
}
