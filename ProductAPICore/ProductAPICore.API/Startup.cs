using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using ProductAPICore.API.ViewModels;
using ProductAPICore.Model.Core;
using ProductAPICore.Model.Core.Domains;
using ProductAPICore.Model.Persistence;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace ProductAPICore.API
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //below code is to configure the content negotiation weather JSON or XML 
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                setupAction.InputFormatters.Add(new XmlDataContractSerializerInputFormatter());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            // add MigrationsAssembly("ProductAPICore.API") to specify where the migration folder will be created
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ProductAPICore.API")));

            //Change from IdentityUser to ApplicationUser to make login and register work
            //also in _LoginPartial.cshtml check comment for corresponding changes
            services.AddDefaultIdentity<ApplicationUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //Add this line to register UnitOfWork in order to make use of DI 
            //where it will pass an object of type IUnitOfWork for each controller
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //registering Swagger for documentation
            services.AddSwaggerGen(setupAction =>
            {
                // use: https://localhost:<port number>/swagger/ProductOpenAPISpecification/swagger.json
                setupAction.SwaggerDoc("ProductOpenAPISpecification", new Info()
                {
                    Title = "Product API",
                    Version = "1",
                    Description = "By using this API you can list and edit products"
                });

                //to read comments and action summary
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            //Configure automapper
            AutoMapper.Mapper.Initialize(config =>
            {
                config.CreateMap<Product, GetProductViewModel>()
                    .ForMember(dest => dest.CompanyName,
                        opt => opt.MapFrom(src => src.Company.Name))
                    .ForMember(dest => dest.CompanyId,
                        opt => opt.MapFrom(src => src.Company.Id));


                config.CreateMap<Product, UpdateProductViewModel>();

                config.CreateMap<UpdateProductViewModel, Product>();


                config.CreateMap<Company, GetCompanyViewModel>();
            });

            //EnsureSeedDataForContext() is used to seed database and added in case of development only to avoid 
            //problems in production and not to mess with data
            unitOfWork.EnsureSeedDataForContext();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                //swagger/index.html
                setupAction.SwaggerEndpoint("/swagger/ProductOpenAPISpecification/swagger.json",
                    "Product API");
                //make default route open swagger documentation by default
                setupAction.RoutePrefix = "";
            });

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();


        }
    }
}
