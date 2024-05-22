using FluentValidation;
using FluentValidation.AspNetCore;
using IoT.SmartZone.Service.Api.Extensions;
using IoT.SmartZone.Service.Api.Options;
using IoT.SmartZone.Service.Application;
using IoT.SmartZone.Service.Application.Common.Exceptions;
using IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;
using Microsoft.Extensions.Options;

public class Startup
{
    private IHostEnvironment _env { get; set; }

    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplicationSetup();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddControllers(options =>
        {
            options.Filters.Add(new HttpResponseExceptionFilter(_env));
        })
        .AddJsonOptions(options =>
        {
            //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

        services
            .AddRouting(options => options.LowercaseUrls = true)
            .AddMediatR(options => options.RegisterServicesFromAssemblies(typeof(DumbRequest).Assembly))
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<DumbRequestValidator>();
        
        services.AddConfigurationOptions(_configuration);

        var connectionStrings = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
        services.SetupDatabase(connectionStrings.SqlDatabase);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IoT.SmartZone.Service.Api v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}