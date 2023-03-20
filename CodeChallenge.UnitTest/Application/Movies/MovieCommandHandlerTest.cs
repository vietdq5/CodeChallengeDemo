using AutoMapper;
using CodeChallenge.Application.Features.Movies.Commands;
using CodeChallenge.Application.Mappings;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest.Application;

public class MovieCommandHandlerTest
{
    private readonly Mock<ILogger<MovieCommandHandler>> _logger;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private MovieCommandHandler _movieCommandHandler;
    private readonly CancellationTokenSource cts;
    private readonly Mock<IMovieRepository> _movieRepository;

    public MovieCommandHandlerTest()
    {
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }
        _logger = new Mock<ILogger<MovieCommandHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        cts = new CancellationTokenSource();
        _movieRepository = new Mock<IMovieRepository>();
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_Create_Movie()
    {
        // Arrange
        var createCommand = new CreateMovieCommand()
        {
            Title = "Test1",
            ReleaseDate = DateTime.UtcNow,
            DirectorId = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };
        var resultObject = new Movie(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        resultObject.Title = "Test1";
        _movieRepository.Setup(s => s.AddAsync(It.IsAny<Movie>())).ReturnsAsync(resultObject);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _movieCommandHandler = new MovieCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieCommandHandler.Handle(createCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(createCommand.Title, result.Title);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_Update_Movie()
    {
        // Arrange
        var updateCommand = new UpdateMovieCommand()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"),
            Title = "Test1",
            ReleaseDate = DateTime.UtcNow,
            DirectorId = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };
        var resultObject = new Movie(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        resultObject.Title = "Test1";
        _movieRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _movieRepository.Setup(s => s.UpdateAsync(It.IsAny<Movie>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _movieCommandHandler = new MovieCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieCommandHandler.Handle(updateCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(updateCommand.Id, result.Id);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task MovieHandle_Return_Successfully_When_Delete_Movie()
    {
        // Arrange
        var deleteCommand = new DeleteMovieCommand()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };
        var resultObject = new Movie(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        resultObject.Title = "Test1";
        _movieRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _movieRepository.Setup(s => s.DeleteAsync(It.IsAny<Movie>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(m => m.MovieRepository).Returns(_movieRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _movieCommandHandler = new MovieCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _movieCommandHandler.Handle(deleteCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(deleteCommand.Id, result.Id);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }
}