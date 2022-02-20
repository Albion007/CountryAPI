using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CountryAPI.models
{
    public class CountryContext: DbContext
    {
        public CountryContext(DbContextOptions<CountryContext> options)
            : base(options)
        {
        }

        public DbSet<CountryItem> CountryItems { get; set; } = null!;
    }
}
