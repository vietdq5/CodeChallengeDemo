using AutoMapper;
using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Application.Features.Directors.Commands;
using CodeChallenge.Application.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Application.Features.Directors.Queries;

public class DirectorQueryHandler : IRequestHandler<GetAllDirectorQuery, IReadOnlyList<DirectorDto>>,
    IRequestHandler<GetDirectorByIdQuery, DirectorDto>
{
    private readonly ILogger<DirectorQueryHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DirectorQueryHandler(ILogger<DirectorQueryHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<DirectorDto>> Handle(GetAllDirectorQuery request, CancellationToken cancellationToken)
    {
        var directors = await _unitOfWork.DirectorRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<DirectorDto>>(directors);
    }

    public async Task<DirectorDto> Handle(GetDirectorByIdQuery request, CancellationToken cancellationToken)
    {
        var director = await _unitOfWork.DirectorRepository.GetByIdAsync(request.Id);
        return _mapper.Map<DirectorDto>(director);
    }
}