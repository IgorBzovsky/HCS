﻿using HCS.Core.Domain;
using HCS.Data;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace HCS.Auth
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private ILogger _logger;
        public Startup(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _logger = loggerFactory.CreateLogger("CertInfoLogger");
            _env = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            X509Certificate2 cert = null;
            using (X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    "036ced5b977e06202a7699e2b461e44eb6d7abcb",
                    false);
                // Get the first cert with the thumbprint
                if (certCollection.Count > 0)
                {
                    cert = certCollection[0];
                    _logger.LogInformation($"Successfully loaded cert from registry: {cert.Thumbprint}");
                }
            }
            // Fallback to local file for development
            if (cert == null)
            {
                cert = new X509Certificate2(Path.Combine(_env.ContentRootPath, "hcs.pfx"), "1111");
                _logger.LogInformation($"Falling back to cert from file. Successfully loaded: {cert.Thumbprint}");
            }

            services.AddDbContext<HcsDbContext>(builder =>
                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=hcsdb;Trusted_Connection=True;MultipleActiveResultSets=true"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<HcsDbContext>();

            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                //.AddTestUsers(Config.Users.All());
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<Config.IdentityProfileService>();
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityConstants.ExternalScheme;
                    options.ClientId = "932208734950-uvvn459764luouis7rusise15ii8ushq.apps.googleusercontent.com";
                    options.ClientSecret = "fQ4noFbG_ib2PaYNWfRhI1LA";
                });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
