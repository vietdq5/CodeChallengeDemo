using AutoMapper;
using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Application.Exceptions;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Application.Features.Directors.Commands;

public class DirectorCommandHandler : IRequestHandler<CreateDirectorCommand, DirectorDto>,
    IRequestHandler<UpdateDirectorCommand, DirectorDto>,
    IRequestHandler<DeleteDirectorCommand, DirectorDto>
{
    private readonly ILogger<DirectorCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DirectorCommandHandler(ILogger<DirectorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<DirectorDto> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = _mapper.Map<Director>(request);

        var result = await _unitOfWork.DirectorRepository.AddAsync(director);
        await _unitOfWork.CompleteAsync();
        _logger.LogInformation($"Director {result.Id} is successfully added.");

        return _mapper.Map<DirectorDto>(result);
    }

    public async Task<DirectorDto> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = await _unitOfWork.DirectorRepository.GetByIdAsync(request.Id);
        if (director == null)
        {
            throw new NotFoundException(nameof(DirectorDto), request.Id);
        }
        _mapper.Map(request, director, typeof(UpdateDirectorCommand), typeof(Director));

        await _unitOfWork.DirectorRepository.UpdateAsync(director);
        await _unitOfWork.CompleteAsync();
        _logger.LogInformation($"Director {director.Id} is successfully updated.");
        return _mapper.Map<DirectorDto>(director);
    }

    public async Task<DirectorDto> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = await _unitOfWork.DirectorRepository.GetByIdAsync(request.Id);
        if (director == null)
        {
            throw new NotFoundException(nameof(DirectorDto), request.Id);
        }
        await _unitOfWork.DirectorRepository.DeleteAsync(director);
        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"Director {director.Id} is successfully deleted.");
        return _mapper.Map<DirectorDto>(director);
    }
}