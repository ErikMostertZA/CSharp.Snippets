using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SqlDataReader.DataAccess.Contexts;
using System.Text;

namespace SqlDataReader
{
  internal class DataReader
  {
    private IDatabaseContext _context;
    public DataReader(IDatabaseContext context)
    {
      _context = context;
    }

    internal void ListMoviesWithLinq()
    {
      Console.ForegroundColor = ConsoleColor.Green;
      var movies = _context.Movies.ToList();

      var sb = new StringBuilder();

      movies.ForEach(movie =>
      {
        sb.AppendLine($"{nameof(movie.Id)}:           {movie.Id}");
        sb.AppendLine($"{nameof(movie.Title)}:        {movie.Title}");
        sb.AppendLine($"{nameof(movie.Genre)}:        {movie.Genre}");
        sb.AppendLine($"{nameof(movie.ReleaseDate)}:  {movie.ReleaseDate}");
        sb.AppendLine("");
        sb.AppendLine("===================================================");
        sb.AppendLine("");
      });

      Console.Write(sb.ToString());
      Console.ResetColor();
    }
    
    internal void ListMoviesWithLinqAndSql()
    {
      Console.ForegroundColor = ConsoleColor.DarkMagenta;
      var movies = _context.Movies.FromSqlRaw("select * from movie").ToList();

      var sb = new StringBuilder();

      movies.ForEach(movie =>
      {
        sb.AppendLine($"{nameof(movie.Id)}:           {movie.Id}");
        sb.AppendLine($"{nameof(movie.Title)}:        {movie.Title}");
        sb.AppendLine($"{nameof(movie.Genre)}:        {movie.Genre}");
        sb.AppendLine($"{nameof(movie.ReleaseDate)}:  {movie.ReleaseDate}");
        sb.AppendLine("");
        sb.AppendLine("===================================================");
        sb.AppendLine("");
      });

      Console.Write(sb.ToString());
      Console.ResetColor();
    }

    internal void ListMoviesWithDataReader()
    {
      Console.ForegroundColor = ConsoleColor.Red;

      var movies = ExecuteSql("select * from movie");

      var sb = new StringBuilder();

      for (int i = 0; i < movies.Count; i++)
      {
        foreach (var key in movies[i].Keys)
          sb.AppendLine($"{key}\t{movies[i][key]}");

        sb.AppendLine("");
        sb.AppendLine("===================================================");
        sb.AppendLine("");
      }

      Console.Write(sb.ToString());
      Console.ResetColor();
    }

    private List<Dictionary<string, object>> ExecuteSql(string sql)
    {

      var cmd = new SqlCommand
      {
        Connection = (SqlConnection)_context.Database.GetDbConnection(),
        CommandText = sql
      };

      if (cmd.Connection.State == System.Data.ConnectionState.Closed)
        cmd.Connection.Open();


      List<Dictionary<string, object>> items = new List<Dictionary<string, Object>>();
      var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        var item = new Dictionary<string, Object>();
        items.Add(item);

        for (int i = 0; i < reader.FieldCount; i++)
        {
          item.Add(reader.GetName(i), reader[i]);
        }
      }

      if (cmd.Connection.State == System.Data.ConnectionState.Open)
        cmd.Connection.Close();

      return items;
    }
  }
}
