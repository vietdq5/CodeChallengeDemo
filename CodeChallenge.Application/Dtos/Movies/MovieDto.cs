using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Dtos.Movies;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public Guid DirectorId { get; set; }

    public DateTime ReleaseDate { get; set; }

    public short? Rating { get; set; }

    public DirectorDto Director { get; set; }
}