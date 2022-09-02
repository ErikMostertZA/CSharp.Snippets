using Microsoft.EntityFrameworkCore;
using SqlDataReader.DataAccess.Models;

namespace SqlDataReader.DataAccess.Contexts
{
  internal class DatabaseContext : DbContext, IDatabaseContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }

  }
}
