using AutoMapper;
using cat_mash_api.Database;
using cat_mash_api.Configuration;
using cat_mash_api.Helpers;
using cat_mash_api.Database.Mapping;
using cat_mash_api.Database.Shared.Interfaces;
using cat_mash_api.Database.Manager;
using cat_mash_api.Business.Interfaces;
using cat_mash_api.Business.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using cat_mash_api.Configuration.Constants;

namespace cat_mash_api
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment HostingEnvironment { get; set; }
        readonly string CorsOrigins = "_myAllowSpecificOrigins";

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"secrets/appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();

            Configuration = builder.Build();
            HostingEnvironment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddControllers();
            services.AddHealthChecks();

            services.AddMvcCore()
                .AddAuthorization()
                .AddApiExplorer();

            // In a case an external authentication server is used.
            // Check StaturpHelpers.cs class to see implementation.
            //services.AddAuthenticationServices(Configuration);

            if (HostingEnvironment.EnvironmentName == "local")
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsOrigins,
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });
            }

            services.Configure<AppSettingsSecret>(Configuration.GetSection("AppSettingsSecret"));
            var connectionString = Configuration.GetSection("AppSettingsSecret").Get<AppSettingsSecret>().ConnectionString;
            // services.AddDbContext<CatMashDbContext>(options => options.UseNpgsql(connectionString));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToDTOMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.TryAddScoped<ICatManager, CatManager>();
            services.TryAddScoped<ICatBusiness, CatBusiness>();

            services.AddSwaggerGenService(Configuration);
        }

        public void Configure(IApplicationBuilder app /*, CatMashDbContext context */)
        {
            // Database migrations
            // context.Database.Migrate()

            app.UseResponseCompression();
            app.UseRouting();

            // In a case an external authentication server is used.
            // app.UseAuthentication();
            // app.UseAuthorization();

            string basePath = "/";
            if (HostingEnvironment.EnvironmentName == "local")
            {
                app.UseCors(this.CorsOrigins);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                basePath += "catmashservice";
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"https://{httpReq.Host.Value}{basePath}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{(basePath.Length == 1 ? "" : basePath)}/swagger/v1/swagger.json", $"{AuthorizationConsts.ApiScope} V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
