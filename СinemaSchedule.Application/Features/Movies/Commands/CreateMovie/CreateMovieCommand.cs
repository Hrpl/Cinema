using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Movies.Commands.CreateMovie;

public record CreateMovieCommand(CreateMovieDto dto) : IRequest<CustomResult<MovieEntity>>;