using Blazored.LocalStorage;
using FluentValidation;
using Handlers;
using Handlers.Core;
using Handlers.Services;
using Korea.Infrastructure.Extensions;
using Korea.Pipelines;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Korea
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var domain = typeof(UserAllQuery).GetTypeInfo().Assembly;

            services.AddMediatR(domain);

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddTransient<IAuthService, AuthService>();


            services.AddBlazoredLocalStorage();

            //Custom provider auth
            services.AddScoped<AuthenticationStateProvider, FakeAuthenticationStateProvider>();

            //Cookie auth
            //services.AddHttpContextAccessor();
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});            
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //Token auth
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = Configuration["JwtIssuer"],
            //            ValidAudience = Configuration["JwtIssuer"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
            //        };
            //    });

            //Register FluentValidation
            services.AddValidatorsFromAssemblies(new[] { domain });

            //This is middleware for mediatr, the order is importnant

            services.RegisterAllTypes(typeof(IAuthorizationConfig<>), new[] { domain });

            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(PreProcessorBehavior<>));
                        
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(PostProcessorBehavior<,>));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
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

            app.UseRouting();

            //Cookies auth
            app.UseCookiePolicy();
            app.UseAuthentication();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
