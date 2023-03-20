using CodeChallenge.Application.Persistence;
using CodeChallenge.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CodeChallengeContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    public IMovieRepository MovieRepository { get; }
    public IDirectorRepository DirectorRepository { get; }

    public UnitOfWork(
        CodeChallengeContext context,
        ILogger<UnitOfWork> logger,
        IMovieRepository movieRepository,
        IDirectorRepository directorRepository)
    {
        _context = context;
        _logger = logger;
        MovieRepository = movieRepository;
        DirectorRepository = directorRepository;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}