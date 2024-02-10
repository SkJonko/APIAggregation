using APIAggregation;
using APIAggregation.Helpers;

var builder = WebApplication.CreateBuilder(args);


var configurationSettings = new ConfigurationSettings(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.SwaggerGenerator();

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