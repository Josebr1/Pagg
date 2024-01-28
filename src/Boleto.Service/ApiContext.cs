using Microsoft.EntityFrameworkCore;

namespace Boleto.Api
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
          : base(options)
        { }

        public DbSet<Pagg.Core.Entities.Boleto> Boletos { get; set; }
    }
}
