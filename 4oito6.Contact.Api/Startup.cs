using _4oito6.AuditTrail.Middleware;
using _4oito6.Contact.Api.Configuration;
using _4oito6.Contact.Infra.CrossCutting.Ioc;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;

namespace _4oito6.Contact.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new NotImplementedException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services
                .ResolveContact()
                .AddControllers(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();

                    config.Filters.Add(new AuthorizeFilter(policy));

                    foreach (var outputFormatter in config.OutputFormatters.OfType<ODataOutputFormatter>().Where(f => f.SupportedMediaTypes.Count == 0))
                    {
                        outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                    }

                    foreach (var inputFormatter in config.InputFormatters.OfType<ODataOutputFormatter>().Where(f => f.SupportedMediaTypes.Count == 0))
                    {
                        inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                    }

                    config.EnableEndpointRouting = false;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<AuditTrailMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"{(string.IsNullOrEmpty(c.RoutePrefix) ? "." : "..")}/swagger/v1/swagger.json", "Template Web API v1"));

            app.UseOData("odata", "", EdmModelBuilder.BuildEdmModel());
        }
    }
}