using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlDataReader.DataAccess.Models
{
  [Table("Movie")]
  internal class Movie
  {

    public Movie(string title, string genre, DateTime releaseDate)
    {
      Title = title;
      Genre = genre;
      ReleaseDate = releaseDate;
    }

    [Key]
    public int Id { get; private set; }
    public string? Title { get; private set; }

    [DataType(DataType.Date)]
    public DateTime ReleaseDate { get; private set; }
    public string? Genre { get; private set; }
  }
}
