using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Swagger;

using APISample.Filters;
using APISample.Middleware;

namespace APISample
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
            //加入cross domain
            services.AddCors();

            //加入過濾器
            services.AddMvc(config =>
                {
                    config.Filters.Add(typeof (ExceptionFilter));
                    config.Filters.Add<ResultFilter>();
                }
            );

            //loggin
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddFile("app.log", append: true);
            });



            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "APISample API",
                    Description = "APISample - ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "SHIH-WU CHENG",
                        Email = string.Empty
                    }
                });
            });           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 錯誤處理
                app.UseErrorHandling();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PushProxy API V1");
            });


            app.UseMvc();

            //使用wwwroot靜態檔案
            app.UseStaticFiles();

            //預設文件
            app.UseDefaultFiles();

            app.UseFileServer();
        }
    }
}
