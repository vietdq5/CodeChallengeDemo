using CodeChallenge.Application.Dtos.Directors;
using MediatR;

namespace CodeChallenge.Application.Features.Directors.Queries;

public class GetAllDirectorQuery : IRequest<IReadOnlyList<DirectorDto>>
{
}