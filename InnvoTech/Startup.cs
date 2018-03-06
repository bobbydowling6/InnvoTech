using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InnvoTech.Models;

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
            services.AddMvc();
            services.AddAntiforgery();
            services.AddSession();

            //This will read the appsettings.json into an object which I can use throughout my app
            //services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            //services.AddOptions();

            //services.AddDbContext<IdentityDbContext>(opt =>
            //opt.UseInMemoryDatabase("Identities")
            //opt.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BobTest; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False"
            //, sqlOptions => sqlOptions.MigrationsAssembly(this.GetType().Assembly.FullName))
            //);
            //opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Added the configuration settings as "ConfigureServices" method through using the sql server context:
            services.AddDbContext<BobTestContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                sqlOptions => sqlOptions.MigrationsAssembly(this.GetType().Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BobTestContext>()
                .AddDefaultTokenProviders();

            //Configure the service for dependency injection
            services.AddTransient<SendGrid.SendGridClient>((x) =>
            {
                return new SendGrid.SendGridClient(Configuration["sendgrid"]);
            });

            services.AddTransient<Braintree.BraintreeGateway>((x) =>
            {
                return new Braintree.BraintreeGateway(
            Configuration["braintree.environment"], Configuration["braintree.merchantid"],
            Configuration["braintree.publickey"], Configuration["braintree.privatekey"]
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BobTestContext Context)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //I'm going to put my initialization logic in a seperate class
            DbInitializer.Initialize(Context);
        }
    }
}
