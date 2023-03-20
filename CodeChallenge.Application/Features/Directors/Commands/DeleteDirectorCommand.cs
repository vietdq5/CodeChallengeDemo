using CodeChallenge.Application.Dtos.Directors;
using MediatR;

namespace CodeChallenge.Application.Features.Directors.Commands;

public class DeleteDirectorCommand : IRequest<DirectorDto>
{
    public Guid Id { get; set; }
}