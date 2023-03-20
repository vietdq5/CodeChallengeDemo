using AutoMapper;
using CodeChallenge.Application.Features.Directors.Commands;
using CodeChallenge.Application.Mappings;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest.Application;

public class DirectorCommandHandlerTest
{
    private readonly Mock<ILogger<DirectorCommandHandler>> _logger;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private DirectorCommandHandler _directorCommandHandler;
    private readonly CancellationTokenSource cts;
    private readonly Mock<IDirectorRepository> _directorRepository;

    public DirectorCommandHandlerTest()
    {
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }
        _logger = new Mock<ILogger<DirectorCommandHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        cts = new CancellationTokenSource();
        _directorRepository = new Mock<IDirectorRepository>();
    }

    [Fact]
    public async Task DirectorHandle_Return_Successfully_When_Create_Director()
    {
        // Arrange
        var createCommand = new CreateDirectorCommand()
        {
            Name = "Test1",
            Birthdate = DateTime.UtcNow
        };
        var resultObject = new Director(Guid.NewGuid());
        resultObject.Name = "Test1";
        _directorRepository.Setup(s => s.AddAsync(It.IsAny<Director>())).ReturnsAsync(resultObject);
        _unitOfWork.Setup(m => m.DirectorRepository).Returns(_directorRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _directorCommandHandler = new DirectorCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _directorCommandHandler.Handle(createCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(createCommand.Name, result.Name);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DirectorHandle_Return_Successfully_When_Update_Director()
    {
        // Arrange
        var updateCommand = new UpdateDirectorCommand()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"),
            Name = "Test1",
            Birthdate = DateTime.UtcNow
        };
        var resultObject = new Director(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        resultObject.Name = "Test1";
        _directorRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _directorRepository.Setup(s => s.UpdateAsync(It.IsAny<Director>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(m => m.DirectorRepository).Returns(_directorRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _directorCommandHandler = new DirectorCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _directorCommandHandler.Handle(updateCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(updateCommand.Id, result.Id);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DirectorHandle_Return_Successfully_When_Delete_Director()
    {
        // Arrange
        var deleteCommand = new DeleteDirectorCommand()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };
        var resultObject = new Director(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        resultObject.Name = "Test1";
        _directorRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _directorRepository.Setup(s => s.DeleteAsync(It.IsAny<Director>())).Returns(Task.CompletedTask);
        _unitOfWork.Setup(m => m.DirectorRepository).Returns(_directorRepository.Object);
        _unitOfWork.Setup(m => m.CompleteAsync()).Callback(() => { });

        // Act
        _directorCommandHandler = new DirectorCommandHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _directorCommandHandler.Handle(deleteCommand, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(deleteCommand.Id, result.Id);
        _unitOfWork.Verify(m => m.CompleteAsync(), Times.Once);
    }
}