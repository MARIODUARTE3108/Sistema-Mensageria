using Microsoft.IdentityModel.Logging;
using Projeto05.Services.Api.Settings;

namespace Projeto05.Services.Api.Configurations
{
    public static class LogConfiguration
    {
        public static void Add(WebApplicationBuilder builder)
        {
            builder.Services.Configure<LogSettings>(builder.Configuration.GetSection("LogSettings"));

            builder.Services.AddTransient<Helpers.LogHelper>();
        }
    }
}
