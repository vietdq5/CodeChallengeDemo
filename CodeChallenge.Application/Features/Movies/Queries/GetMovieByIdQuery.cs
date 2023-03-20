using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Movies.Queries;

public class GetMovieByIdQuery : IRequest<MovieDto>
{
    public Guid Id { get; set; }
}