using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Application.Dtos.Movies;
using MediatR;

namespace CodeChallenge.Application.Features.Directors.Commands;

public class CreateDirectorCommand : IRequest<DirectorDto>
{
    public string Name { get; set; }

    public DateTime Birthdate { get; set; }
}