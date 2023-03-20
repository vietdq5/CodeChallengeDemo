using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Movies.Queries;

public class GetAllMovieQuery : IRequest<IReadOnlyList<MovieDto>>
{
}