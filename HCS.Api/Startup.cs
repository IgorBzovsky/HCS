using AutoMapper;
using HCS.Core;
using HCS.Core.Domain;
using HCS.Data;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System;

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
            services.Configure<AppSettings>(options =>
            {
                Configuration.GetSection("AppSettings").Bind(options);
            });

            var provider = services.BuildServiceProvider();
            var settings = provider.GetService<IOptions<AppSettings>>();
            services.AddAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(settings.Value.BaseUrl.Web)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddAuthorization(options => {
                    options.AddPolicy(RolePolicies.AdminPolicy, policy => policy.RequireClaim(JwtClaimTypes.Role, RolePolicies.AdminRole));
                    options.AddPolicy(RolePolicies.ProviderPolicy, policy => policy.RequireClaim(JwtClaimTypes.Role, RolePolicies.ProviderRole));
                })
                .AddJsonFormatters(options => options.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = settings.Value.BaseUrl.Auth;
                    options.RequireHttpsMetadata = false;
                    options.ApiName = settings.Value.ApiName;
                    options.RoleClaimType = JwtClaimTypes.Role;
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HcsDbContext>()
                .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HCS API", Version = "v1" });
                var xmlPath = AppDomain.CurrentDomain.BaseDirectory + @"HCS.Api.xml";
                c.IncludeXmlComments(xmlPath);
            });
            services.AddDbContext<HcsDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
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
