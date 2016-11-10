using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Routing
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var trackPackageRouteHandler = new RouteHandler(async context =>
            {
                var routeValues = context.GetRouteData().Values;
                await context.Response.WriteAsync($"Hello! Route values: {string.Join(", ", routeValues)}");
            });
            var routeBuilder = new RouteBuilder(app, trackPackageRouteHandler);
            routeBuilder.MapRoute("Track package route", "{package?}/{operation:regex(^track|create|detonate$)=create}/{id:int:range(1,5)}/{*anything}");
            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Hi, {name}");
            });
            var routes = routeBuilder.Build();
            app.UseRouter(routes);

            app.Run(async context =>
            {
                var dictionary = new RouteValueDictionary() {{ "operation", "create" }, {"id", 3}, {"package","123"} };
                var vpc = new VirtualPathContext(context, null, dictionary, "Track package route");
                var path = routes.GetVirtualPath(vpc).VirtualPath;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("Menu<hr/>");
                await context.Response.WriteAsync($"<a href=\"{path}\">Create Package 123</a><br/>");
            });


            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}
