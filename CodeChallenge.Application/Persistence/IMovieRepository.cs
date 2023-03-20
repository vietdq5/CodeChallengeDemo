using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Persistence;

public interface IMovieRepository : IAsyncRepository<Movie>
{
    Task<IReadOnlyList<Movie>> GetAllByDirectorIdAsync(Guid directorId);
}