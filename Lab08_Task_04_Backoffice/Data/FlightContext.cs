using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Backoffice.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options)
            : base(options)
        {
        }

        public DbSet<FlightClient> FlightClients { get; set; } = null!;
    }
}