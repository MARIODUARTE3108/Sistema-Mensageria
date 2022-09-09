using Projeto05.Services.Api.Helpers;
using Projeto05.Services.Api.Settings;

namespace Projeto05.Services.Api.Configurations
{
    public static class MailConfiguration
    {
        public static void Add(WebApplicationBuilder builder)
        {
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddTransient<EmailHelper>();
        }
    }
}
