using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SqlDataReader.DataAccess.Models;

namespace SqlDataReader.DataAccess.Contexts
{
  internal interface IDatabaseContext
  {
    DatabaseFacade Database { get; }
    DbSet<Movie> Movies { get; set; }
  }
}
