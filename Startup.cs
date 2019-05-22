using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.AspNetCore.DataProtection.AzureStorage;
using MVC_DotNet.Models;

namespace MVC_DotNet
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
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/Account/Login");
                o.AccessDeniedPath = new PathString("/Home/Error");
            });
            services.AddMvc();//这个放后面
            //services.AddSession();
            var redisConn="127.0.0.1:6379";
            var redis = StackExchange.Redis.ConnectionMultiplexer.Connect(redisConn);
            
             services.AddSingleton(redis as StackExchange.Redis.IConnectionMultiplexer);//在所有的地方所有的请求会话创建的都是相同的
             services.AddDistributedRedisCache(option =>
            {       

                   //redis 数据库连接字符串
                   option.Configuration = redisConn;
                   //redis 实例名
                  option.InstanceName = "master";//hash值有master
              });//session用redis
            // services.AddDistributedSqlServerCache(o =>
            //   {
            //       o.ConnectionString = "server=10.1.1.10;database=aaaaa;uid=sa;pwd=P@ssw0rd;";
            //       o.SchemaName = "dbo";
            //       o.TableName = "SessionState";
            //   });
            // services.AddSession(o =>
            // {
            //     o.IdleTimeout = TimeSpan.FromSeconds(1800);
            // });
              services.AddScoped<IOperation,bar>();//每一个不同的会话创建一个不同的实例
              //AddTransient 每一个服务和每个控制器都创建一个不同的实例
              //AddSingleton 创建的对象在所有的地方所有的请求会话创建的都是相同的
                //services.AddSingleton<IOperationSingleton, Operation>();
                // services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
              services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Index}/{id?}");
            });
        }
    }
}
