using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkShopNET5.Model;
using WorkShopNET5.Model.Interface;
using WorkShopNET5.Model.Repository;

namespace WorkShopNET5
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddDbContext<StoreMISPortalDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MISPortalDB")));
            services.AddScoped<IHrStoreRepository, HrStoreRepository>();
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;

                // Redirect to an external URL
                if (url.Contains("/api/login"))
                {

                    String uri = Configuration["redirect_uri"];
                    String oauthUrl = "" + Configuration["oauth_authorize_url"] + "?response_type=code&client_id=" + Configuration["client_id"] + "&redirect_uri=" + Configuration["redirect_uri"] + "&scope=" + Configuration["oauth_scope"];
                    context.Response.Redirect(oauthUrl);
                    return;   // short circuit
                }

                await next();
            });

          

        }
    }
}
