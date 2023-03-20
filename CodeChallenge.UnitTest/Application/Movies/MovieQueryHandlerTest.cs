using AutoMapper;
using CodeChallenge.Application.Features.Movies.Queries;
using CodeChallenge.Application.Mappings;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest.Application;

public class MovieQueryHandlerTest
{
    private readonly Mock<ILogger<MovieQueryHandler>> _logger;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private MovieQueryHandler _movieQueryHandler;
    private readonly CancellationTokenSource cts;
    private readonly Mock<IMovieRepository> _movieRepository;

    public MovieQueryHandlerTest()
    {
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }
        _logger = new Mock<ILogger<MovieQueryHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        cts = new CancellationTokenSource();
        _movieRepository = new Mock<IMovieRepository>();
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_GetAll_Movie()
    {
        // Arrange
        var listReuslt = new List<Movie>()
        {
            new Movie(Guid.NewGuid()),
            new Movie(Guid.NewGuid()),
        };

        _movieRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(listReuslt);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);

        // Act
        _movieQueryHandler = new MovieQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieQueryHandler.Handle(new GetAllMovieQuery(), cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_GetById_Movie()
    {
        // Arrange
        var query = new GetMovieByIdQuery()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };

        var resultObject = new Movie(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        _movieRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);

        // Act
        _movieQueryHandler = new MovieQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieQueryHandler.Handle(query, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(query.Id, result.Id);
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_GetByDirectorId_Movie()
    {
        // Arrange
        var query = new GetMovieByDirectorIdQuery()
        {
            DirectorId = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };
        var listReuslt = new List<Movie>()
        {
            new Movie(Guid.NewGuid()),
            new Movie(Guid.NewGuid()),
        };

        _movieRepository.Setup(s => s.GetAllByDirectorIdAsync(It.IsAny<Guid>())).ReturnsAsync(listReuslt);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);

        // Act
        _movieQueryHandler = new MovieQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieQueryHandler.Handle(query, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(2, result.Count);
    }
}