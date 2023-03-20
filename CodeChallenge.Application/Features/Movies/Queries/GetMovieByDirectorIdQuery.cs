using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Movies.Queries;

public class GetMovieByDirectorIdQuery : IRequest<IReadOnlyList<MovieDto>>
{
    public Guid DirectorId { get; set; }
}