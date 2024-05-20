using FluentValidation.AspNetCore;
using HRMS.Data.Comman.Helpers;
using HRMS.Data.Data;
using HRMS.Data.Identity;
using HRMS.Extensions;
using HRMS.Middleware;
using HRMS.Services.UriService;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HRMS
{
    public class StartUp
    {
        private readonly IConfiguration _config;

            public StartUp(IConfiguration configuration) 
            { 
                _config = configuration;

            }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                    fv.RegisterValidatorsFromAssemblyContaining<StartUp>();
                });
            services.AddRazorPages();
            services.AddOptions();
     
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(_config.GetConnectionString("HRMS")));
            services.AddDbContext<HrmsContext>(options => options.UseSqlServer(_config.GetConnectionString("HRMS")));

            services.AddLazyCache();
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddConfig();
            services.AddIdentityService(_config);
            services.ConfigureApplicationServices(_config);
            services.AddHttpClient();
            services.AddSession(option =>
            option.IdleTimeout = TimeSpan.FromMinutes(60));
            services.AddHttpContextAccessor();
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: "CorsPolicy", policy =>
                {
                    if (_config["AllowedOrigins"].Trim() != "*")
                    {
                        var origins = _config["AllowedOrigins"].Split(";");
                        policy.WithOrigins(origins)
                                            //.SetIsOriginAllowed(origin => true)
                                            //.AllowCredentials()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    }
                    else
                    {
                        policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }

                });
            });
            services.AddSingleton<IUriService>(o =>
            {
                var accessor = o.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(uri);
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hrms.API", Version = "v1" });
                c.IgnoreObsoleteActions();
            });
        }
        public void Configure (IApplicationBuilder app, IWebHostEnvironment ev)
        {
            if (ev.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project.API v1 "));
            }
               app.UseMiddleware<ExceptionMiddleWare>();

                _ = bool.TryParse(_config["SecureApi:E2EE:Request:Enabled"], out bool e2eeEnabled);
                if(e2eeEnabled) app.UseMiddleware<SecureApiDecrytionalMiddleWare>();


                app.UseStatusCodePagesWithReExecute("api/errors/{0}");
                app.UseHttpsRedirection();

                app.UseRouting();
                app.UseCors("CorsPolicy");
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseStaticFiles();
                app.UseSession();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            
            
        }
    }
}
