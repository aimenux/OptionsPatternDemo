﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ReloadConfigChangesDemo.Api.Services;

namespace ReloadConfigChangesDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
            ConfigureSettings(services);
            ConfigureIoc(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureSettings(IServiceCollection services)
        {
            services.Configure<Flags>(Configuration.GetSection("Flags"));
        }

        private void ConfigureIoc(IServiceCollection services)
        {
            var choice = Configuration.GetValue<int>("Choice");

            switch (choice)
            {
                case 1:
                    services.AddSingleton<IConfigurationService>(serviceProvider =>
                    {
                        var options = serviceProvider.GetRequiredService<IOptions<Flags>>();
                        return new ConfigurationService(options);
                    });
                    break;
                case 2:
                    services.AddScoped<IConfigurationService>(serviceProvider =>
                    {
                        var options = serviceProvider.GetRequiredService<IOptionsSnapshot<Flags>>();
                        return new ConfigurationService(options);
                    });
                    break;
                case 3:
                    services.AddSingleton<IConfigurationService>(serviceProvider =>
                    {
                        var options = serviceProvider.GetRequiredService<IOptionsMonitor<Flags>>();
                        return new ConfigurationService(options);
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice));
            }
        }
    }
}
