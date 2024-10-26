using Sentry.WebApp.Authorization;
using Sentry.WebApp.Authorization.Handlers;
using Sentry.WebApp.Authorization.Policies;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models.PowerBi;
using Sentry.WebApp.Services;
using Sentry.WebApp.Services.PowerBi;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
//using Microsoft.Identity.Web;
//using Microsoft.Identity.Web.TokenCacheProviders.InMemory;
using System;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Sentry.WebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Config>(Configuration);
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddSession(opts =>
            {
                opts.Cookie.Name = ".Sentry.Session";
                opts.IdleTimeout = TimeSpan.FromMinutes(15);
                opts.Cookie.HttpOnly = true;
                opts.Cookie.IsEssential = true;
            });

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(opts =>
                {
                    opts.Instance = Configuration.GetSection("AzureAd")["Instance"];
                    opts.Domain = Configuration.GetSection("AzureAd")["Domain"];
                    opts.CallbackPath = Configuration.GetSection("AzureAd")["CallbackPath"];
                    opts.SignedOutCallbackPath = Configuration.GetSection("AzureAd")["SignedOutCallbackPath"];
                    opts.TenantId = Configuration.GetSection("AzureAd")["TenantId"];
                    opts.ClientId = Configuration.GetSection("AzureAd")["ClientId"];
                    opts.ClientSecret = Configuration.GetSection("AzureAd")["ClientSecret"];
                },
                cookie =>
                {
                    cookie.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                    //cookie.LogoutPath = "/esecurity/logoff";
                    cookie.SlidingExpiration = true;

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AdministratorsAuthorizationPolicy.Name,
                                  AdministratorsAuthorizationPolicy.Build);
                options.AddPolicy(DataManagementAuthorizationPolicy.Name,
                                  DataManagementAuthorizationPolicy.Build);
                options.AddPolicy(FinanceAuthorizationPolicy.Name,
                                  FinanceAuthorizationPolicy.Build);
                options.AddPolicy(HumanResourcesAuthorizationPolicy.Name,
                                  HumanResourcesAuthorizationPolicy.Build);
                options.AddPolicy(RecordsQualityAuthorizationPolicy.Name,
                                  RecordsQualityAuthorizationPolicy.Build);
                options.AddPolicy(SentryUsersAuthorizationPolicy.Name,
                                  SentryUsersAuthorizationPolicy.Build);
            });

            services.AddScoped(typeof(AadService))
                    .AddScoped(typeof(PbiEmbedService));

            services.Configure<AzureAd>(Configuration.GetSection("AzureAd-PowerBI"))
                    .Configure<PowerBI>(Configuration.GetSection("PowerBI"));

            services.AddSingleton<IAuthorizationHandler, AdministratorsHandler>();
            services.AddSingleton<IAuthorizationHandler, DataManagementHandler>();
            services.AddSingleton<IAuthorizationHandler, FinanceHandler>();
            services.AddSingleton<IAuthorizationHandler, HumanResourcesHandler>();
            services.AddSingleton<IAuthorizationHandler, RecordsQualityHandler>();
            services.AddSingleton<IAuthorizationHandler, SentryUsersHandler>();

            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPdfService, PDFService>();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddJsonOptions(opts => { opts.AllowInputFormatterExceptionMessages = true; });

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AppDbContext")
                , sqlServerOptions => sqlServerOptions.CommandTimeout(Convert.ToInt16(Configuration["SqlServerCommandTimeout"]))));
            services.AddDbContext<DwDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DwDbContext")));

            string[] filteredGroups = new string[] {
                Configuration["AzureAd:Sentry_Admins_GroupId"],
                Configuration["AzureAd:Data_Steward_HR_GroupId"],
                Configuration["AzureAd:Data_Steward_Financial_Services_GroupId"],
                Configuration["AzureAd:Data_Steward_Records_Quality_GroupId"],
                Configuration["AzureAd:Data_Management_GroupId"],
                Configuration["AzureAd:UDP_GroupId"],
                Configuration["AzureAd:AP_Reviewers_GroupId"],
                Configuration["AzureAd:AP_General_Counsel_GroupId"],
                Configuration["AzureAd:AP_Assistant_VP_GroupId"],
                Configuration["AzureAd:AP_VP_GroupId"],
                Configuration["AzureAd:AP_CFO_GroupId"],
                Configuration["AzureAd:AP_Managers_GroupId"],
                Configuration["AzureAd:AP_Scholarship_Managers_GroupId"],
                Configuration["AzureAd:AR_Gift_Processing_GroupId"],
                Configuration["AzureAd:AR_Staff_GroupId"],
                Configuration["AzureAd:AR_SupervisorReviewers_GroupId"],
                Configuration["AzureAd:AR_GUStaff_GroupId"],
                Configuration["AzureAd:FT_Reviewers_GroupId"],
                Configuration["AzureAd:FT_Approvers_GroupId"],
                Configuration["AzureAd:FT_GeneralCounselApprover_GroupId"]
            };

            services.AddScoped<IClaimsTransformation, FilterGroupClaimsTransformation>(parameters => new FilterGroupClaimsTransformation(filteredGroups));

            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = 3048;
            });

            services.AddRazorPages().AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    //.RequireClaim("groups", Configuration.GetValue<string>("AzureAd:UAFDN_GroupId"))
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) //IHostingEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/SystemError");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}/{SystemId?}"
                );

                endpoints.MapControllerRoute(
                    name: "fieldHistory",
                    pattern: "{controller=Integration}/{action=FieldHistory}/{FieldId}/{IntegrationId}/{SystemId}/{RecordId}"
                    );

                endpoints.MapRazorPages();
            });

        }
    }
}