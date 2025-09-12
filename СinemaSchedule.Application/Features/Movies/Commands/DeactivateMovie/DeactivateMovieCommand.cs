using MediatR;
using СinemaSchedule.Domen.Entities;
using СinemaSchedule.Domen.Generic;

namespace СinemaSchedule.Application.Features.Movies.Commands.DeactivateMovie;

public record DeactivateMovieCommand(int MovieId) : IRequest<MbResult<Unit>>;