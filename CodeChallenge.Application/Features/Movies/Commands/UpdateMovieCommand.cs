﻿using CodeChallenge.Application.Dtos.Movies;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Application.Features.Movies.Commands;

public class UpdateMovieCommand : IRequest<MovieDto>
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Guid DirectorId { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Range(0, 10)]
    public short? Rating { get; set; }
}