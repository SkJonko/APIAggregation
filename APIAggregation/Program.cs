using APIAggregation;
using APIAggregation.Helpers;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);


var configurationSettings = new ConfigurationSettings(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerGenerator();



builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
{
    string appVersion = typeof(Program).Assembly.GetName().Version.ToString();


    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .Enrich.WithExceptionDetails()
        .ReadFrom.Configuration(builder.Configuration)

        //
        // Custom properties
        //
        .Enrich.WithProperty(nameof(appVersion), appVersion)
        .Enrich.WithProperty("ApplicationName", "ApiAggregation");

    var SeqConnString = builder.Configuration.GetValue<string>("LoggingData:SerilogUrl");

    if (SeqConnString is not null)
        loggerConfiguration.WriteTo.Seq(SeqConnString);

    loggerConfiguration.WriteTo.Console();
});

builder.Services.AddServicesInDI(configurationSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware(typeof(HandlingMiddleware));

app.UseReDoc(options =>
{
    options.DocumentTitle = "Swagger Documentation";
    options.SpecUrl = "/swagger/v1/swagger.json";
    options.HideHostname();
});

app.Run();