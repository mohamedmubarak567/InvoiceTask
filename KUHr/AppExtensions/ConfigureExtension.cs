using System;
using System.IO;
using KUHr.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace KUHr.WebAPI.AppExtensions
{
    /// <inheritdoc />
    public static class ConfigureExtension
    {
        /// <inheritdoc />
        public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.CorsConfig();
            app.DevelopmentSetupConfig(env);
            app.UseHttpsRedirection();
            app.StaticFiles(env);
            app.UseRouting();           
            app.SwaggerConfig(configuration);
            app.CreateDataBase();
            //
            
            //
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });


        


            return app;          
        }
        private static void CreateDataBase(this IApplicationBuilder app)
        {
            try
            {
                using var scope = app.ApplicationServices.CreateScope();
                using var context = scope.ServiceProvider.GetService<KUHrContext>();
                context.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
      
        

       
        private static void SwaggerConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var swaggerEndPoint = configuration.GetValue<string>("SwaggerEndPoint");
                c.SwaggerEndpoint(swaggerEndPoint, "KUHr");
                c.DocumentTitle = "TAMS Api Documentation";
                c.DocExpansion(DocExpansion.None);
            });
        }

        private static void StaticFiles(this IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseStaticFiles();
            var path = Path.Combine(env.ContentRootPath, "Files");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/Files",
                EnableDirectoryBrowsing = true
            });
        }
        private static void CorsConfig(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
        private static void DevelopmentSetupConfig(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            else app.UseHsts();
        }      
    }
}
