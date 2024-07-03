using FluentValidation;
using HrmsSystemAdmin.Web.Commond.Auth;
using HrmsSystemAdmin.Web.Extensions;
using HrmsSystemAdmin.Web.Infrastructure.Mapper;
using HrmsSystemAdmin.Web.MiddleWare;
using HrmsSystemAdmin.Web.Services.http.HrmsApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HrmsSystemAdmin.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllersWithViews(config => config.Filters.Add(new AuthorizeFilter()));
            services.AddAuthenticationService(_config);
            services.AddApplicatonServices();
            services.AddHostedApplicationServices();
            services.ConfigureApplicationAndServices(_config);

            services.AddHttpClient();
            services.AddHttpClient(HrmsApiDefaults.HttpClientHrmsApi, client =>
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(_config.GetValue<string>("HrmsApi:BaseUrl"));
                client.Timeout = new TimeSpan(0, 0, 600);
            });
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");
            app.UseCookiePolicy();

            app.UseSession();
            //app.UseNotyf();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            // Auth Middleware
            app.UseMiddleware<AuthMiddleWare>();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
