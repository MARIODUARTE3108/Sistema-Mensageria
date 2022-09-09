using Microsoft.EntityFrameworkCore;
using Projeto05.Services.Api.Contexts;
using Projeto05.Services.Api.Produce;
using Projeto05.Services.Api.Settings;

namespace Projeto05.Services.Api.Configurations
{
    public static class RabbitMQConfiguration
    {
        public static void Add(WebApplicationBuilder builder)
        {
            builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings"));

            builder.Services.AddTransient<ProduceMessage>();
        }
    }
}
