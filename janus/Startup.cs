using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using overapp.janus.Configurations;
using overapp.janus.Infrastructure;
using overapp.janus.Infrastructure.Repositories;
using overapp.janus.Infrastructure.Services;
using overapp.janus.Managers;
using Polly;
using Polly.Extensions.Http;

namespace overapp.janus
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
            // Bind configurations
            services.Configure<OpenApiConfig>(Configuration.GetSection("OpenApiConfig"));

            // Swagger
            services.AddSwaggerService(Configuration);

            // Automapper
            services.AddAutoMapper(typeof(Startup));

            // DI
            services.AddTransient<IPaymentManager, PaymentManager>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IMerchantRepository, MerchantRepository>();

            services.AddHttpClient<IBankService, BankService>(opt =>
                {
                    opt.BaseAddress = Configuration.GetValue<Uri>("ExternalServices:BankAPI");
                    opt.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                })
                .AddPolicyHandler(GetBankApiRetryPolicy())
                .AddPolicyHandler(GetBankApiCircuitBreakerPolicy());

            // If MS Sql available switch add Microsoft.EntityFrameworkCore.SqlServer and switch to SQL instead of in mem.
            //services.AddDbContext<JanusContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Janus")));
            services.AddDbContext<JanusContext>(options => options.UseInMemoryDatabase(databaseName: Configuration["DataStorage:DbName"]));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            var swaggerConfig = Configuration.GetSection(typeof(OpenApiConfig).Name).Get<OpenApiConfig>();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(swaggerConfig.EndpointUrl, swaggerConfig.EndpointName);
                option.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IAsyncPolicy<HttpResponseMessage> GetBankApiRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetBankApiCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(15));
        }

    }

    internal static class CustomExtensionsMethods
    {   
        internal static IServiceCollection AddSwaggerService(this IServiceCollection services, IConfiguration configuration)
        {
            var OpenApiConfig = configuration.GetSection(typeof(OpenApiConfig).Name).Get<OpenApiConfig>();
            services.AddSwaggerGen(setupAction =>
            {
                // Register the Swagger generator, defining 1 or more Swagger documents
                setupAction.SwaggerDoc(OpenApiConfig.Version, new OpenApiInfo { Title = OpenApiConfig.Title, Version = OpenApiConfig.Version });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                setupAction.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}