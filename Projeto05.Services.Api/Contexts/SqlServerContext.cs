using Microsoft.EntityFrameworkCore;
using Projeto05.Services.Api.Contexts.Entities;

namespace Projeto05.Services.Api.Contexts
{
    public class SqlServerContext : DbContext
    {
        public SqlServerContext(DbContextOptions<SqlServerContext> options) : base(options)
        {

        }
        public DbSet<Usuario>? Usuarios { get; set; }
    }
}
