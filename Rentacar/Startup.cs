using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Rentacar.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rentacar.Areas.Admin.Service;
using Rentacar.BusinessLogic;
using Rentacar.BusinessLogic.IdentityLogic;
using Rentacar.BusinessLogic.IdentityLogic.Interfaces;
using Rentacar.BusinessLogic.Interface;
using Rentacar.BusinessLogic.Order;
using Rentacar.BusinessLogic.Rent;
using Rentacar.DataAccess;
using Rentacar.DataAccess.Data;
using Rentacar.DataAccess.Data.Repository.IRepository;
using Rentacar.DataAccess.Data.Repository;
using Rentacar.Utility;
using IdentityOptions = Microsoft.AspNetCore.Identity.IdentityOptions;
using Rentacar.DataAccess.DbInitializer;

namespace Rentacar
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

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            });
               
            
            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()    
                .AddDefaultTokenProviders()
                .AddDefaultUI();
                
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 3;
                options.SignIn.RequireConfirmedEmail = true;
            });

            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            
            
          // services.AddScoped<IRolesLogic, RolesLogic>();
          //// services.AddScoped<IRolesLogic, RolesLogic>();
          services.AddScoped<IRegisterLogic, RegisterLogic>();
          services.AddScoped<IFactory, Factory>();
           services.AddScoped<IRolesLogic, RolesLogic>();
            services.AddScoped<IBrandLogic, BrandLogic>();
            services.AddScoped<ICarLogic, CarLogic>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IFuelLogic, FuelLogic>();
            services.AddScoped<ILogger, Logger<Car>>();
            services.AddScoped<ILoginLogic, LoginLogic>();
            services.AddScoped<ILogOut, LogOut>();
            services.AddScoped<IUserDetails, UserDetails>();
            services.AddScoped<IRentDetailsLogic, RentDetailsLogic>();
            services.AddScoped<IRentLogic, RentLogic>();
            services.AddScoped<IOrderLogic, OrderLogic>();


            //services.AddScoped<IShoppingCartLogic, IShoppingCartLogic>();
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Admin/Identity/SignIn";
                options.AccessDeniedPath = "/Admin/Identity/AccessDenied";
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
            });

            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddAuthentication().AddCookie();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", p =>
                {
                    p.RequireRole(Roles.Admin);
                });
            });

            // services.AddMvc(options =>
            // {
            //     var policy = new AuthorizationPolicyBuilder()
            //         .RequireAuthenticatedUser().Build();
            //     options.Filters.Add(new AuthorizeFilter(policy));
            // }).AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to  configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            dbInitializer.Initializer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
