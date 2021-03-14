    using AugustusIntegrations.Cache;
    using AugustusIntegrations.ExternalAPI;
    using AugustusWebApp.Facade;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.SpaServices.AngularCli;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using StackExchange.Redis;

    namespace AugustusMartin
    {
        public class Startup
        {
            public IConfiguration Configuration { get; }

            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddControllersWithViews();

                // In production, the Angular files will be served from this directory
                services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });

                #region Redis Dependencies
           
                services.AddSingleton(sp =>
                {
                  var configuration = ConfigurationOptions.Parse(Configuration.GetConnectionString("Redis"), true);
                  return ConnectionMultiplexer.Connect(configuration);
                });
            
           
                #endregion

                #region Project Dependencies
                services.AddScoped<ICacheStorage, RedisCacheStorage>();
                services.AddScoped<IUserFacade, UserFacade>();
                services.AddScoped<IPostFacade, PostFacade>();            
                services.AddScoped<IRestClient>(restClient => new JSONPlaceHolderRestClient(Configuration.GetValue<string>("JsonPlaceHolderBaseAddress")));
                #endregion

                #region Swagger Dependencies

                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Augustus API", Version = "v1" });
                });

                #endregion

            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                if (!env.IsDevelopment())
                {
                    app.UseSpaStaticFiles();
                }

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller}/{action=Index}/{id?}");
                });

                app.UseSwagger();

                app.UseSwaggerUI(sw =>
                {
                    sw.SwaggerEndpoint("/swagger/v1/swagger.json", "Augustus API v1");
                });


                app.UseSpa(spa =>
                {
                    // To learn more about options for serving an Angular SPA from ASP.NET Core,
                    // see https://go.microsoft.com/fwlink/?linkid=864501

                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });


            }
        }
    }
