﻿using Microsoft.OpenApi.Models;
using Prestadito.Setting.API.Controller;
using Prestadito.Setting.Application.Manager.Endpoints;
using Prestadito.Setting.Application.Manager.Extensions;
using Prestadito.Setting.Application.Manager.Interfaces;
using Prestadito.Setting.Application.Services.Interfaces;
using Prestadito.Setting.Application.Services.Services;
using Prestadito.Setting.Infrastructure.Data.Settings;
using Prestadito.Setting.Infrastructure.MainModule.Extensions;

namespace Prestadito.Setting.API
{
    public static class WebApplicationHelper
    {
        readonly static string myCors = "myCors";

        public static WebApplication CreateWebApplication(this WebApplicationBuilder builder)
        {
            var provider = builder.Services.BuildServiceProvider();

            var configuration = provider.GetRequiredService<IConfiguration>();

            builder.Services.AddMongoDbContext(configuration);

            builder.Services.AddScoped<IDataService, DataService>();
            builder.Services.AddScoped<IParametersController, ParameterController>();

            builder.Services.AddValidators();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prestadito.Micro.Setting.API",
                    Description = "ASP.NET Core Web API Control Schedule System",
                    TermsOfService = new Uri("https://prestadito.pe/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Prestadito.pe",
                        Email = "contacto@prestadito.pe",
                        Url = new Uri("https://prestadito.pe"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://prestadito.pe"),
                    }
                });
            });

            builder.Services.AddHealthChecks()
                .AddCheck<MongoDBHealthCheck>(nameof(MongoDBHealthCheck));

            builder.Services.AddCors(options =>
            {
                var urlList = configuration.GetSection("AllowedOrigin").GetChildren().Select(c => c.Value).ToArray();
                options.AddPolicy(myCors,
                    builder => builder.WithOrigins(urlList)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return builder.Build();
        }

        public static WebApplication ConfigureWebApplication(this WebApplication app)
        {
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "Prestadito.Micro.Setting.API");
            });
            //}

            app.UseCors(myCors);
            app.UseSettingEndpoint(myCors);

            return app;
        }
    }
}