using CodeChallenge.Domain.Common;

namespace CodeChallenge.Domain.Entities;

public class Director : EntityBase
{
    public string Name { get; set; }

    public DateTime Birthdate { get; set; }

    public ICollection<Movie> Movies { get; set; }

    public Director()
    {
    }

    public Director(Guid id)
    {
        Id = id;
    }
}