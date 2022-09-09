using Microsoft.EntityFrameworkCore;
using Projeto05.Services.Api.Contexts;

namespace Projeto05.Services.Api.Configurations
{
    public static class SqlServerConfiguration
    {
        public static void Add(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("BDProjeto05"); 
            builder.Services.AddDbContext<SqlServerContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
