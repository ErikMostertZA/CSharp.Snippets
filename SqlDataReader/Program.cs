using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlDataReader;
using SqlDataReader.DataAccess.Contexts;
using SqlDataReader.DataAccess.Models;
using Microsoft.Extensions.Logging;

const string dbConnection = "Server=(localdb)\\mssqllocaldb;Database=MovieContext;Trusted_Connection=True;MultipleActiveResultSets=true";

// Configure Dependency Injection
using IHost host = Host.CreateDefaultBuilder(args)
                       .ConfigureServices((_, services) =>
                                      services.AddTransient<IDatabaseContext, DatabaseContext>()
                                              .AddTransient<DataReader>()
                                              .AddDbContext<DatabaseContext>(options => options.UseSqlServer(dbConnection)))
                       .ConfigureLogging((logging) =>
                       {
                         // clear default logging providers
                         logging.ClearProviders();
                       })
                       .Build();

DoSomething(host.Services);

await host.RunAsync();

static void DoSomething(IServiceProvider services)
{

  using IServiceScope serviceScope = services.CreateScope();
  IServiceProvider provider = serviceScope.ServiceProvider;

  SeedData.Initialize(provider);

  DataReader reader = provider.GetRequiredService<DataReader>();

  reader.ListMoviesWithLinq();
  reader.ListMoviesWithLinqAndSql();
  reader.ListMoviesWithDataReader();
}