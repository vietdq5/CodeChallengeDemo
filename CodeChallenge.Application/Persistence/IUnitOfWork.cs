namespace CodeChallenge.Application.Persistence;

public interface IUnitOfWork
{
    IMovieRepository MovieRepository { get; }
    IDirectorRepository DirectorRepository { get; }
    Task CompleteAsync();
}