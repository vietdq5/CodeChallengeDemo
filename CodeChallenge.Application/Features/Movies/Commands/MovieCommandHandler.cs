using AutoMapper;
using CodeChallenge.Application.Dtos.Movies;
using CodeChallenge.Application.Exceptions;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Application.Features.Movies.Commands;

public class MovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieDto>,
    IRequestHandler<UpdateMovieCommand, MovieDto>,
    IRequestHandler<DeleteMovieCommand, MovieDto>
{
    private readonly ILogger<MovieCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MovieCommandHandler(ILogger<MovieCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<MovieDto> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = _mapper.Map<Movie>(request);

        var result = await _unitOfWork.MovieRepository.AddAsync(movie);
        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"Movie {result.Id} is successfully added.");

        return _mapper.Map<MovieDto>(result);
    }

    public async Task<MovieDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);
        if (movie == null)
        {
            throw new NotFoundException(nameof(MovieDto), request.Id);
        }
        _mapper.Map(request, movie, typeof(UpdateMovieCommand), typeof(Movie));

        await _unitOfWork.MovieRepository.UpdateAsync(movie);
        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"Movie {movie.Id} is successfully updated.");
        return _mapper.Map<MovieDto>(movie);
    }

    public async Task<MovieDto> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _unitOfWork.MovieRepository.GetByIdAsync(request.Id);
        if (movie == null)
        {
            throw new NotFoundException(nameof(MovieDto), request.Id);
        }
        await _unitOfWork.MovieRepository.DeleteAsync(movie);
        await _unitOfWork.CompleteAsync();

        _logger.LogInformation($"Movie {movie.Id} is successfully deleted.");
        return _mapper.Map<MovieDto>(movie);
    }
}