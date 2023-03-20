using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Directors.Commands;

public class UpdateDirectorCommand : IRequest<DirectorDto>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Birthdate { get; set; }
}