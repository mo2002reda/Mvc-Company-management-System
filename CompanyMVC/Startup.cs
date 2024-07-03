using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using CompanyMVC.MappProfiles;
using CompanyMVC.ViewModels;
using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace CompanyMVC
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
            services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });//Allow Depandancy Injection
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUniteOfWork, UniteOfWork>();
            //services.AddAutoMapper(o => o.AddProfile(new EmployeeProfile()));
            //instead of register many profiles in startup we can use one AutoMapper for all profiles which carry list of profiles 
            services.AddAutoMapper(o => o.AddProfiles(new List<Profile>() { new EmployeeProfile(), new UserProfile(), new RoleProfile() }));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;

            })//To Add Interfaces Which Have funcitons of Identity Such as :CreateAsync() which exist in userManager

                .AddEntityFrameworkStores<CompanyDbContext>()//to Add Classes which implemented interfaces which have Identity Functions
                .AddDefaultTokenProviders();//to generate tokens(specified the roles of users) for users 

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)//Cookie = Key of Encrypt & Decrypt
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, Options =>
                    {
                        Options.LoginPath = new PathString("/Account/Login");
                        Options.AccessDeniedPath = new PathString("/Home/Error");
                    });
            // .AddAuthentication() allaw dependancy Injection for three services=> {SignIn Manager - UserManager - Role Manager} instead of addScopped For each service

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Register}");
            });
        }
    }
}
