using AutoMapper;
using CodeChallenge.Application.Dtos.Movies;
using CodeChallenge.Application.Features.Movies.Commands;
using CodeChallenge.Application.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Application.Features.Movies.Queries;

public class MovieQueryHandler : IRequestHandler<GetAllMovieQuery, IReadOnlyList<MovieDto>>,
    IRequestHandler<GetMovieByIdQuery, MovieDto>,
    IRequestHandler<GetMovieByDirectorIdQuery, IReadOnlyList<MovieDto>>
{
    private readonly ILogger<MovieQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MovieQueryHandler(ILogger<MovieQueryHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<MovieDto>> Handle(GetAllMovieQuery request, CancellationToken cancellationToken)
    {
        var movies = await _unitOfWork.MovieRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<MovieDto>>(movies);
    }

    public async Task<MovieDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<IReadOnlyList<MovieDto>> Handle(GetMovieByDirectorIdQuery request, CancellationToken cancellationToken)
    {
        var movies = await _unitOfWork.MovieRepository.GetAllByDirectorIdAsync(request.DirectorId);
        return _mapper.Map<IReadOnlyList<MovieDto>>(movies);
    }
}