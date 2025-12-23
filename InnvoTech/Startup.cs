using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using InnvoTech.Models;
using SendGrid;
using Braintree;
using SmartyStreets;

namespace InnvoTech
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAntiforgery();
            services.AddSession();

            // Add DbContext
            services.AddDbContext<BobTestContext>(
                opt => opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<BobTestContext>()
                .AddDefaultTokenProviders();

            // Configure external service clients for dependency injection
            services.AddTransient<SendGridClient>((x) =>
            {
                return new SendGridClient(Configuration["sendgrid"]);
            });

            services.AddTransient<BraintreeGateway>((x) =>
            {
                return new BraintreeGateway(
                    Configuration["braintree.environment"],
                    Configuration["braintree.merchantid"],
                    Configuration["braintree.publickey"],
                    Configuration["braintree.privatekey"]
                );
            });

            services.AddTransient<SmartyStreets.USStreetApi.Client>((x) =>
            {
                var client = new ClientBuilder(
                    Configuration["smartystreets.authid"],
                    Configuration["smartystreets.authtoken"])
                    .BuildUsStreetApiClient();

                return client;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env != null && "Development".Equals(env.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // Initialize database
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BobTestContext>();
                DbInitializer.Initialize(context);
            }
        }
    }
}
