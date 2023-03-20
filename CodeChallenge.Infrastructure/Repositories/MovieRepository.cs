using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Infrastructure.Persistence;

namespace CodeChallenge.Infrastructure.Repositories;

public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
{
    public MovieRepository(CodeChallengeContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Movie>> GetAllByDirectorIdAsync(Guid directorId)
    {
        return await GetAsync(s => s.DirectorId == directorId,includeString:nameof(Movie.Director));
    }
}