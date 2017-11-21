using HyperMediaApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace HyperMediaApi
{
    public class Startup
    {
        private readonly int? _httpsPort ;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            if (env.IsDevelopment())
            {
                var launchConfig = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("Properties\\launchSettings.json")
                .Build();

                _httpsPort = launchConfig.GetValue<int>("iisSettings:iisExpress:sslPort");
            }

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //solution for Web API with EF Core - Returning child data along with parent (via Include)
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddMvc(options =>
            {
                options.SslPort = _httpsPort;
                options.Filters.Add(typeof(RequireHttpsAttribute));
                options.Filters.Add(typeof(JsonExceptionFilter));
                var jsonFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().SingleOrDefault();
                options.OutputFormatters.Remove(jsonFormatter);
               // options.OutputFormatters.Add(new IonOutPutFormatter(jsonFormatter));
            });

            services.AddRouting(o => o.LowercaseUrls = true);

            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionSelector = new CurrentImplementationApiVersionSelector(o);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts(opt =>
            {
                opt.MaxAge(days: 180);
                opt.IncludeSubdomains();
                opt.Preload();
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "api/{controller=Default}/{action=Get}/{id?}");
            });
        }
    }
}
