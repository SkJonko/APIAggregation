using APIAggregation;
using APIAggregation.Helpers;
using Asp.Versioning;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);


var configurationSettings = new ConfigurationSettings(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerGenerator();


builder.Host.UseSerilog((hostBuilderContext, serviceProvider, loggerConfiguration) =>
{
    string appVersion = typeof(Program).Assembly.GetName().Version!.ToString();


    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .Enrich.WithExceptionDetails()
        .ReadFrom.Configuration(builder.Configuration)

        //
        // Custom properties
        //
        .Enrich.WithProperty("AppVersion", appVersion)
        .Enrich.WithProperty("ApplicationName", "ApiAggregation");



    var SeqConnString = builder.Configuration.GetValue<string>("LoggingData:SerilogUrl");

    if (SeqConnString is not null)
        loggerConfiguration.WriteTo.Seq(SeqConnString);

    loggerConfiguration.WriteTo.Console();


    //
    // Run this to create the docker Aspire.
    // docker run --rm -it -p 18888:18888 -p 4317:18889 -d --name aspire-dashboard mcr.microsoft.com/dotnet/nightly/aspire-dashboard:8.0.0-preview.6
    //
    var aspireUrl = builder.Configuration.GetValue<string>("LoggingData:AspireUrl");

    if (aspireUrl is not null)
        loggerConfiguration.WriteTo.OpenTelemetry(options =>
        {
            options.Endpoint = aspireUrl;
            options.ResourceAttributes = new Dictionary<string, object>
            {
                ["service.name"] = "ApiAggregation"
            };
            options.IncludedData = IncludedData.MessageTemplateRenderingsAttribute |
                                  IncludedData.TraceIdField | IncludedData.SpanIdField |
                                  IncludedData.MessageTemplateTextAttribute | IncludedData.SourceContextAttribute;
        });
});

builder.Services.AddServicesInDI(configurationSettings);

builder.Services.AddApiVersioning(versionOptions =>
{
    versionOptions.AssumeDefaultVersionWhenUnspecified = true;
    versionOptions.DefaultApiVersion = new ApiVersion(1, 0);
    versionOptions.ReportApiVersions = true;
})
    .AddMvc()
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.AddServiceDefaults();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.ConfigureDocumentationUI(app.DescribeApiVersions());


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<HandlingMiddleware>();

app.Run();