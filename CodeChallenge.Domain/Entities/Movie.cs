using CodeChallenge.Domain.Common;

namespace CodeChallenge.Domain.Entities;

public class Movie : EntityBase
{
    public string Title { get; set; }

    public Guid DirectorId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public short? Rating { get; set; }

    public Director Director { get; set; }

    public Movie()
    {
    }

    public Movie(Guid id)
    {
        Id = id;
    }
}