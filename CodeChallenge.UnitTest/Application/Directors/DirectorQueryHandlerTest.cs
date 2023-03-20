using AutoMapper;
using CodeChallenge.Application.Features.Directors.Queries;
using CodeChallenge.Application.Mappings;
using CodeChallenge.Application.Persistence;
using CodeChallenge.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CodeChallenge.UnitTest.Application;

public class DirectorQueryHandlerTest
{
    private readonly Mock<ILogger<DirectorQueryHandler>> _logger;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private DirectorQueryHandler _directorQueryHandler;
    private readonly CancellationTokenSource cts;
    private readonly Mock<IDirectorRepository> _directorRepository;

    public DirectorQueryHandlerTest()
    {
        if (_mapper == null)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }
        _logger = new Mock<ILogger<DirectorQueryHandler>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        cts = new CancellationTokenSource();
        _directorRepository = new Mock<IDirectorRepository>();
    }

    [Fact]
    public async Task DirectorHandle_Return_Successfully_When_GetAll_Director()
    {
        // Arrange
        var listReuslt = new List<Director>()
        {
            new Director(Guid.NewGuid()),
            new Director(Guid.NewGuid()),
        };

        _directorRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(listReuslt);
        _unitOfWork.Setup(m => m.DirectorRepository).Returns(_directorRepository.Object);

        // Act
        _directorQueryHandler = new DirectorQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _directorQueryHandler.Handle(new GetAllDirectorQuery(), cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task DirectorHandle_Return_Successfully_When_GetById_Director()
    {
        // Arrange
        var query = new GetDirectorByIdQuery()
        {
            Id = Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96")
        };

        var resultObject = new Director(Guid.Parse("23BA36B0-C502-4A8D-128E-08DB292A8E96"));
        _directorRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(resultObject);
        _unitOfWork.Setup(m => m.DirectorRepository).Returns(_directorRepository.Object);

        // Act
        _directorQueryHandler = new DirectorQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
        var result = await _directorQueryHandler.Handle(query, cts.Token);

        // Assert
        result.Should().NotBeNull();
        Assert.Equal(query.Id, result.Id);
    }
}