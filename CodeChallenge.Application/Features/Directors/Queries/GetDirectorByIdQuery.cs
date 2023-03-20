using CodeChallenge.Application.Dtos.Directors;
using MediatR;

namespace CodeChallenge.Application.Features.Directors.Queries;

public class GetDirectorByIdQuery : IRequest<DirectorDto>
{
    public Guid Id { get; set; }
}