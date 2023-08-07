using cat_mash_api.Configuration.Constants;
using cat_mash_api.Database;
using cat_mash_api.Database.Shared.EntityModels;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;

namespace cat_mash_api.Helpers
{
    public static class StartupHelpers
    {
        // In a case an external authentication server is used.
        /* public static void AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration.GetSection("AppSettings").Get<AppSettings>().AuthenticationServerUri;
                    options.RequireHttpsMetadata = true;

                    options.Audience = AuthorizationConsts.ApiScope;
                });
        } */

        public static void AddSwaggerGenService(this IServiceCollection services, IConfiguration configuration)
        {
            //var authUri = configuration.GetSection("AppSettings").Get<AppSettings>().AuthStsUri;

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AuthorizationConsts.ApiScope,
                    Version = "v1",
                    Description = "CatMash API"
                });

                // Swagger login configuration in a case we need it
                /*
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri(authUri + "/connect/authorize"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { AuthorizationConsts.ApiScope, "CatMashService" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { AuthorizationConsts.ApiScope }
                    }
                });
                */


                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}