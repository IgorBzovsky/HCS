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
using HCS.Data;
using Microsoft.EntityFrameworkCore;
using HCS.Core;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using HCS.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace HCS.Api
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
            services.AddAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            services.AddMvcCore()
                .AddAuthorization(options => {
                    options.AddPolicy("admin", policy => policy.RequireClaim("role", "admin"));
                })
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5002";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "hcsApi";
                    options.RoleClaimType = "role";
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HCS API", Version = "v1" });
                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"HCS.Api.xml";
                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<HcsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HcsDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HCS API V1");
            });
        }
    }
}
