using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlDataReader.DataAccess.Contexts;

namespace SqlDataReader.DataAccess.Models
{
  internal static class SeedData
  {
    internal static void Initialize(IServiceProvider serviceProvider)
    {
      using var context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());

      if (context.Movies.Any())
        return;

      context.Movies.AddRange
      (
        new Movie("When Harry met Sally", "Romantic Comedy", DateTime.Parse("1989-02-12")),
        new Movie("Ghostbusters", "Comedy", DateTime.Parse("1984-03-13")),
        new Movie("Ghostbusters 2", "Comedy", DateTime.Parse("1986-02-23")),
        new Movie("Rio Bravo", "Western", DateTime.Parse("1959-04-15"))
      );

      context.SaveChanges();
    }
  }
}
