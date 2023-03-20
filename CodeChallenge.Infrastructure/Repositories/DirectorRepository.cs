using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Infrastructure.Persistence;

namespace CodeChallenge.Infrastructure.Repositories;

public class DirectorRepository : RepositoryBase<Director>, IDirectorRepository
{
    public DirectorRepository(CodeChallengeContext dbContext) : base(dbContext)
    {
    }
}