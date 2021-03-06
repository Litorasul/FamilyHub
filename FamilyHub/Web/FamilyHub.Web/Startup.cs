﻿namespace FamilyHub.Web
{
    using System;
    using System.Reflection;

    using FamilyHub.Common;
    using FamilyHub.Data;
    using FamilyHub.Data.Common;
    using FamilyHub.Data.Common.Repositories;
    using FamilyHub.Data.Models;
    using FamilyHub.Data.Repositories;
    using FamilyHub.Data.Seeding;
    using FamilyHub.Services.Data;
    using FamilyHub.Services.Data.Lists;
    using FamilyHub.Services.Data.Messenger;
    using FamilyHub.Services.Data.PhotoAlbum;
    using FamilyHub.Services.Data.Planner;
    using FamilyHub.Services.Data.User;
    using FamilyHub.Services.Data.WallPosts;
    using FamilyHub.Services.Data.Weather;
    using FamilyHub.Services.Mapping;
    using FamilyHub.Services.Messaging;
    using FamilyHub.Web.Areas.Identity;
    using FamilyHub.Web.Hubs;
    using FamilyHub.Web.ViewModels;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<CustomSignInManager>();

            services.Configure<CookiePolicyOptions>(options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddSignalR();
            services.AddControllersWithViews().AddSessionStateTempDataProvider();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            services.Configure<CloudinarySettings>(this.configuration.GetSection("CloudinarySettings"));
            services.Configure<OpenWeatherSettings>(this.configuration.GetSection("OpenWeatherSettings"));

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IListsService, ListsService>();
            services.AddTransient<IWallPostsService, WallPostsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IMessengerService, MessengerService>();
            services.AddTransient<IPhotoAlbumsService, PhotoAlbumsService>();
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHub<MessengerHub>("/Messenger/Message");
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(
                        "eventByName",
                        "/Events/{name:minlength(3)}",
                        new { controller = "Events", action = "ByName" });
                    endpoints.MapControllerRoute(
                        "PhotoAlbumByName",
                        "/Photos/{name:minlength(3)}",
                        new { controller = "Photos", action = "ByName" });
                    endpoints.MapControllerRoute(
                        "listByName",
                        "/Lists/{name:minlength(3)}",
                        new { controller = "Lists", action = "ByName" });
                    endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
        }
    }
}
