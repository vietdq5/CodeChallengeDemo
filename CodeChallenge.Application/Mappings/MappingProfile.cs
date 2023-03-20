using AutoMapper;
using CodeChallenge.Application.Dtos.Directors;
using CodeChallenge.Application.Dtos.Movies;
using CodeChallenge.Application.Features.Directors.Commands;
using CodeChallenge.Application.Features.Movies.Commands;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Director
        CreateMap<Director, DirectorDto>().ReverseMap();
        CreateMap<CreateDirectorCommand, Director>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateDirectorCommand, Director>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        // Movies
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<CreateMovieCommand, Movie>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UpdateMovieCommand, Movie>().ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}