using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Middleware.Data;
using Middleware.Models;
using Middleware.Services;
using Microsoft.AspNetCore.Http;

namespace Middleware
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseDatabaseErrorPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();

            //app.UseIdentity();

            //// Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello World");
            //});
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello World again!");
            //});
            //ConfigureEnvironmentOne(app,env);
            ConfigureMapping(app);
            ConfigureMapWhen(app);
            ConfigureEnvironmentTwo(app, env);
            ConfigureLogInline(app, env, loggerFactory);

        }


        public void ConfigureLogInline(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Information);
            //var logger = loggerFactory.CreateLogger(env.EnvironmentName);
            //app.Use(async (content, task) =>
            //{
            //    logger.LogInformation("Handling request.");
            //    await task.Invoke();
            //    logger.LogInformation("Finished handling request.");
            //});

            app.UseRequestLogger(); //this middle replaced above comments.

            app.Run(async context =>
            {
                //logger.LogInformation("Responsing...");
                await context.Response.WriteAsync("Hello from " + env.EnvironmentName);
            });
        }

        public void ConfigureEnvironmentOne(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Hello from {env.EnvironmentName}");
            });
        }

        public void ConfigureEnvironmentTwo(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, task) =>
            {
                await context.Response.WriteAsync($"Hello from {env.EnvironmentName}");
                await task.Invoke(); // if havenot called this method, will not invoked next middleware.
            });
        }

        public void ConfigureMapping(IApplicationBuilder app)
        {
            app.Map("/maptest", HandleMapTest);
        }


        private void HandleMapTest(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map test successfully!");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context => await context.Response.WriteAsync("Branch used"));
        }

        public void ConfigureMapWhen(IApplicationBuilder app)
        {
            app.MapWhen((context) => context.Request.Query.ContainsKey("branch"), HandleBranch);
        }
    }
}
