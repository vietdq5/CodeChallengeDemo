using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Movies.Commands;

public class DeleteMovieCommand : IRequest<MovieDto>
{
    public Guid Id { get; set; }
}