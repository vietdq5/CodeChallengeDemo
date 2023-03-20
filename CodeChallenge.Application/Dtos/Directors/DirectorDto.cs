using CodeChallenge.Application.Dtos.Movies;

namespace CodeChallenge.Application.Dtos.Directors;

public class DirectorDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Birthdate { get; set; }
}