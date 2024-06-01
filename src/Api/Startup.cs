using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Exceptions;
using Api.Extensions;
using Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Api;

public class Startup
{
    private static readonly JsonNamingPolicy CamelCase = JsonNamingPolicy.CamelCase;
    
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.AddLogging();
        builder.AddSwaggerDoc();
        builder.Services.AddExceptionHandler<NotValidChoiceExceptionHandler>();
        builder.Services.AddVersioning();
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(CamelCase));
            });
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.Configure<Settings>(builder.Configuration.GetSection(nameof(Settings)));
        builder.Services.AddTransient(GetConfigurationService());
    }

    public void Configure(WebApplication app)
    {
        app.UseExceptionHandler(_ => {});
        app.UseSwaggerDoc();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
    
    private static Func<IServiceProvider, Func<string, IConfigurationService>> GetConfigurationService()
    {
        return serviceProvider => choice =>
        {
            return choice switch
            {
                nameof(Choices.ConfigurationOptionsService) => new ConfigurationService(serviceProvider.GetRequiredService<IOptions<Settings>>()),
                nameof(Choices.ConfigurationOptionsMonitorService) => new ConfigurationService(serviceProvider.GetRequiredService<IOptionsMonitor<Settings>>()),
                nameof(Choices.ConfigurationOptionsSnapshotService) => new ConfigurationService(serviceProvider.GetRequiredService<IOptionsSnapshot<Settings>>()),
                _ => throw new NotValidChoiceException(choice)
            };
        };
    }
}