using CodeChallenge.Application.Persistence;
using CodeChallenge.Infrastructure.Persistence;
using CodeChallenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CodeChallengeContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CodeChallengeConnectionString")));

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IDirectorRepository, DirectorRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}