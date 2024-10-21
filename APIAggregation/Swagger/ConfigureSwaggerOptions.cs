using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIAggregation.Swagger
{
    /// <summary>
    /// Configure Swagger Options for versioning.
    /// </summary>
    /// <remarks>
    /// ctor
    /// </remarks>
    /// <param name="provider"></param>
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) : IConfigureOptions<SwaggerGenOptions>
    {

        #region PRIVATE

        private readonly IApiVersionDescriptionProvider _provider = provider;

        #endregion

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Description and versioning of controllers
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = $"APIAggregation ({Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} @ {Environment.MachineName} v{typeof(Program).Assembly.GetName().Version})",
                Description = "Simple API Aggregation",
                Version = description.ApiVersion.ToString(),
                Contact = new OpenApiContact()
                {
                    Name = "Kotis Antonis",
                    Email = "antkotis@hotmail.com"
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}